// Brandon Cunningham
// C00431388
// CMPS 261
// Program Description: description of actions of code
// Certificate of Authenticity:
// I certify that the code in the method functions
// including method function main of this project are
// entirely my own work.

package com.company;
import java.io.*;
import java.util.*;

public class Main {
    /**
     *
     * @param set A set of strings
     * @param fileName The path of the file
     *
     * <p>
     * This method reads from the file into the passed set of strings, sorts and prints the set.
     *
     */
    public static void sortSetFromFile(Set<String> set, String fileName) {
        File file = new File(fileName);
        Scanner scanner = null;
        try {
            scanner = new Scanner(file);
            while (scanner.hasNextLine()) {
                set.add(scanner.nextLine());
            }
            List<String> list = new ArrayList<>(set);
            Collections.sort(list);
            set = new HashSet<>(list);

            Iterator<String> setIter = set.iterator();
            while (setIter.hasNext())
                System.out.print(setIter.next() + " ");
            System.out.println();

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    /**
     *
     * @param args The command line arguments
     *
     * <p>
     *      This method creates an empty set of strings to pass through the sortSetFromFile method along with the file path for fstein.txt
     *
     */
    public static void main(String[] args) {
        Set<String> set = new HashSet<String>();

        sortSetFromFile(set, "C:\\Users\\Admin\\p71_C00431388\\fstein.txt"); // Problem 1
    }
}
