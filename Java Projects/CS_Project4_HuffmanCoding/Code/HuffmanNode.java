
class HuffmanNode implements Comparable<HuffmanNode> {
    char character; // Character stored in the node
    int frequency; // Frequency of the character
    HuffmanNode left; // Left node
    HuffmanNode right; // Right node

    // Constructor for leaf nodes
        HuffmanNode(char character, int frequency) {
            this.character = character;
            this.frequency = frequency;
        }

    // Constructor for internal nodes
        HuffmanNode(int frequency, HuffmanNode left, HuffmanNode right) {
            this.frequency = frequency;
            this.left = left;
            this.right = right;
        }

    public int compareTo(HuffmanNode node) {
        return Integer.compare(this.frequency, node.frequency);
    }
}

