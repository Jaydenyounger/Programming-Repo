import java.io.IOException;
import java.nio.file.*;

public class main {
    public static void main(String[] args) {
        // file paths
        // if it is throughing errors, copy the file path in your file explorer for each book and place them into the array
        String[] filePaths = {
            /*----------------Paste your file path here------------------ */
            "C:\\Users\\jayde\\Downloads\\CS_Project4_HuffmanCoding\\Files" + "\\book1.txt", // book 1
            "C:\\Users\\jayde\\Downloads\\CS_Project4_HuffmanCoding\\Files" + "\\book2.txt", // book 2
            "C:\\Users\\jayde\\Downloads\\CS_Project4_HuffmanCoding\\Files" + "\\book3.txt", // book 3
            "C:\\Users\\jayde\\Downloads\\CS_Project4_HuffmanCoding\\Files" + "\\book4.txt", // book 4
            "C:\\Users\\jayde\\Downloads\\CS_Project4_HuffmanCoding\\Files" + "\\book5.txt", // book 5
            "C:\\Users\\jayde\\Downloads\\CS_Project4_HuffmanCoding\\Files" + "\\book6.txt"};// book 6

        HuffmanCoding huffman = new HuffmanCoding(); // Initialize a new HuffmanCoding object
            for (String filePath : filePaths) { // Iterate over each element in the filePaths array
                try {
                    String text = Files.readString(Paths.get(filePath));// Read file content
                    huffman.buildtrie(text); // Build Huffman trie and generate codes
                    
                    huffman.displayTop5Symbols(filePath); // Display Top 5 Most Frequent Symbols for each book

                    String encodedText = huffman.encode(text);// Encode the text

                    // Define output filenames
                        String binaryFilename = filePath.replace(".txt", ".bin");
                        String decompressedFilename = filePath.replace(".txt", "_decompressed.txt");
                    
                    huffman.writeEncodedToFile(encodedText, binaryFilename); // Write encoded text to binary file

                    // Calculating and displaying the compression ratio
                        double compressionRatio = huffman.calculateCompressionRatio(text, binaryFilename);
                        System.out.println("Compression Ratio for " + filePath + ": " + compressionRatio);

                    // Decoding the binary file
                        String encodedFromFile = huffman.readEncodedFromFile(binaryFilename);
                        String decodedText = huffman.decode(encodedFromFile);

                    // Write decompressed content to a new file
                        Files.writeString(Paths.get(decompressedFilename), decodedText);

                    // Validate and confirm decoding success
                        System.out.println("Decoding successful for " + filePath + ": " + text.equals(decodedText));

                        System.out.println(); // Space out code

            } catch (IOException e) {
                // catchs a error when processing a file
                    System.out.println("Error processing file: " + filePath);
                    e.printStackTrace();
            }
        }
    }
}
