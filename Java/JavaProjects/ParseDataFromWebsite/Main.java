package com.company;
import java.io.*;
import java.util.*;

public class Main {
    /**
     *
     * @param map A map of any type
     * @param <K> Generic Key
     * @param <V> Generic Value
     *           <p>
     *           This method sorts the passed map in descending order by the value.
     * @return Returns a sorted linked hash map.
     */
    public static <K, V extends Comparable<? super V>> Map<K, V> sortByValue(Map<K, V> map) {
        List<Map.Entry<K, V>> list = new ArrayList<>(map.entrySet());
        list.sort(Map.Entry.comparingByValue());
        Collections.reverse(list);

        Map<K, V> result = new LinkedHashMap<>();
        for (Map.Entry<K, V> entry : list) {
            result.put(entry.getKey(), entry.getValue());
        }
        return result;
    }

    /**
     *
     * @param boys Map for baby boy names
     * @param girls Map for baby girl names
     * @param year Year for baby names
     *             <p>
     *                 Takes the passed map variables and prints out each of the names and values in a ranked list.
     */
    public static void printRankings(Map<String, Integer> boys, Map<String, Integer> girls, String year) {
        Iterator<String> boyIter = boys.keySet().iterator();
        Iterator<String> girlIter = girls.keySet().iterator();

        int i = 1, j = 1;
        System.out.println("Boy Baby Name Rankings of " + year);
        while (boyIter.hasNext()) {
            String key = boyIter.next();
            int value = boys.get(key);
            System.out.println(i + ") " + key + " : " + value);
            i++;
        }
        System.out.println();

        System.out.println("Girl Baby Name Rankings of " + year);
        while (girlIter.hasNext()) {
            String key = girlIter.next();
            int value = girls.get(key);
            System.out.println(j + ") " + key + " : " + value);
            j++;
        }
        System.out.println();
    }

    /**
     *
     * @param std Scanner used for input
     * @return Returns the filled map
     */
    public static Map<String, Integer> fillMap(Scanner std) {
        String key = std.next();
        String value = std.next();

        Map<String, Integer> map = new LinkedHashMap<>();
        map.put(key, Integer.parseInt(value));
        return map;
    }

    /**
     *
     * @param args Command line arguments
     *             <p>
     *             This method prompts the user to enter a year, gender, and name to find the baby name's rank for that year.
     */
    public static void main(String[] args) {
        Map<String, Integer> babyboyRank = new LinkedHashMap<>();
        Map<String, Integer> babygirlRank = new LinkedHashMap<>();
        Scanner in = new Scanner(System.in);

        System.out.print("Enter a year from 2001 to 2010, a gender, and a name (ex. 2001 male Brandon): ");
        String[] inputLine = in.nextLine().split(" ");

        String year = inputLine[0];
        String gender = inputLine[1];
        String name = inputLine[2];

        try {
            Scanner std = new java.util.Scanner(new java.net.URL("http://www.cs.armstrong.edu/liang/data/babynamesranking" + year + ".txt").openStream());

            System.out.println();
            while (std.hasNextLine()) {
                String pass = std.next();
                babyboyRank = fillMap(std);
                babygirlRank = fillMap(std);
            }
            babyboyRank = sortByValue(babyboyRank);
            babygirlRank = sortByValue(babygirlRank);
            printRankings(babyboyRank, babygirlRank, year);

            Map<String, Integer> map = new LinkedHashMap<>();
            if (gender.toLowerCase().equals("male"))
                map = babyboyRank;
            else if (gender.toLowerCase().equals("female"))
                map = babygirlRank;

            if (map.containsKey(name)) {
                Iterator<String> mapIter = map.keySet().iterator();
                for (int i = 1; mapIter.hasNext(); i++) {
                    if (mapIter.next().equals(name)) {
                        System.out.println("Selection [ (" + i + ") " + name + " : " + map.get(name) + " ]");
                        break;
                    }
                }
            } else {
                System.out.println("The name \"" + name + "\" does not exist in the records.");
            }

        } catch(IOException e) {
                e.printStackTrace();
        }
    }
}