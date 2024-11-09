import java.io.*;
import java.util.*;

public class HuffmanCoding {
    private HashMap<Character, String> huffmanCode = new HashMap<>(); // stores Huffman codes
    private HashMap<Character, Integer> frequencyMap = new HashMap<>(); // stores the frequency
    private HuffmanNode root; // Root node of the trie

    // builds the Huffman trie and generate codes
    public void buildtrie(String text) {
        // Counts frequency of each character
            for (char ch : text.toCharArray()) {
                frequencyMap.put(ch, frequencyMap.getOrDefault(ch, 0) + 1);
            }

        // Build priority queue
            PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<>();
                for (var entry : frequencyMap.entrySet()) {
                    priorityQueue.add(new HuffmanNode(entry.getKey(), entry.getValue()));
                }

        // Build Huffman trie
            while (priorityQueue.size() > 1) {
                HuffmanNode left = priorityQueue.poll();
                HuffmanNode right = priorityQueue.poll();
                HuffmanNode newNode = new HuffmanNode(left.frequency + right.frequency, left, right);
                priorityQueue.add(newNode);
            }
        root = priorityQueue.poll(); // Root of Huffman trie
        generateCodes(root, ""); // Generate Huffman Codes
    }





    // generate the Huffman Codes
    private void generateCodes(HuffmanNode node, String code) {
        if (node == null) return;
        // Leaf node, store code
            if (node.left == null && node.right == null) {
                huffmanCode.put(node.character, code); // assign the code to the character
            }
        generateCodes(node.left, code + "0");
        generateCodes(node.right, code + "1");
    }





    // encodes the text using generated Huffman codes
    public String encode(String text) {
        StringBuilder encodedText = new StringBuilder();
            for (char ch : text.toCharArray()) {
                encodedText.append(huffmanCode.get(ch));
            }
        return encodedText.toString();
    }





    // Write encoded binary data to a file
    public void writeEncodedToFile(String encodedText, String filename) throws IOException {
        int len = encodedText.length();
        int padding = 8 - (len % 8); // Calculate padding if the length is not a multiple of 8
        encodedText += "0".repeat(padding); // Add padding 

    // Write the encoded text as bytes to the specified file
        try (FileOutputStream fos = new FileOutputStream(filename)) {
            for (int i = 0; i < encodedText.length(); i += 8) {
                String byteString = encodedText.substring(i, i + 8);
                fos.write((byte) Integer.parseInt(byteString, 2));
            }
        }
    }





    // Calculate compression ratio
    public double calculateCompressionRatio(String text, String binaryFilename) throws IOException {
        File originalFile = new File("text.txt"); 
            try (FileOutputStream fos = new FileOutputStream(originalFile)) {
                fos.write(text.getBytes());
            }

        File binaryFile = new File(binaryFilename);
        return (double) binaryFile.length() / originalFile.length();
    }





    // decode a bit string to its original text
    public String decode(String encodedText) {
        StringBuilder decodedText = new StringBuilder();
        HuffmanNode current = root;

            for (char bit : encodedText.toCharArray()) { // Traverse the Huffman trie based on each bit
                current = (bit == '0') ? current.left : current.right;

                if (current.left == null && current.right == null) {
                    // append the character and reset to root
                        decodedText.append(current.character);
                        current = root;
                }
            }
        return decodedText.toString();
    }





    // Read the binary file and convert it to a bit string
    public String readEncodedFromFile(String filename) throws IOException {
        StringBuilder bitString = new StringBuilder();

        try (FileInputStream fis = new FileInputStream(filename)) {
            int currentByte;
                while ((currentByte = fis.read()) != -1) {
                    bitString.append(String.format("%8s", Integer.toBinaryString(currentByte & 0xFF)).replace(' ', '0'));
                }
            }
        return bitString.toString();
    }




    
    // retrieve the top 5 most frequent symbols and their codes
    public void displayTop5Symbols(String filePath) {
        // Sort the frequency map by value
        List<Map.Entry<Character, Integer>> sortedEntries = new ArrayList<>(frequencyMap.entrySet());
        sortedEntries.sort((a, b) -> b.getValue() - a.getValue());

        // Print the top 5 characters with their frequencies and Huffman codes
        System.out.println("Top 5 Most Frequent Symbols for " + filePath + ": ");
        for (int i = 0; i < Math.min(5, sortedEntries.size()); i++) {
            char symbol = sortedEntries.get(i).getKey();
            int frequency = sortedEntries.get(i).getValue();
            String code = huffmanCode.get(symbol);
            System.out.printf("Symbol: %s (Frequency: %d) -> Huffman Code: %s%n", symbol, frequency, code);
        }
    }
}
