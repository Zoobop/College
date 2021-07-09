// Brandon Cunningham
// C00431388
// CMPS 260
// Programming Assignment : pa5
// Due Date : Nov. 19, 2020
// Program Description: [insert brief description here]
// Certificate of Authenticity:
// I certify that the code in the Main, Photograph, and Camera classes
// of this project is entirely my own work with the exception of the
// example solution to Programming Assignment #4 provided by the
// instructor.

package com.company;

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        // write your code here
        Scanner s = new Scanner(System.in);

        Photograph p1 = Photograph.createAll255sPhotograph();
        System.out.println("createAll255sPhotograph Factory: " + p1.getSize() + " " + p1.getDate());

        Photograph p2 = Photograph.createMinimumPhotograph();
        System.out.println("createMinimumPhotograph Factory: " + p2.getSize() + " " + p2.getDate());

        Photograph p3 = Photograph.create7x7Checkerboard();
        System.out.println("create7x7Checkerboard Factory: " + p3.getSize() + " " + p3.getDate());


        System.out.print("\nEnter the size of the memory card: ");
        int memory = s.nextInt();
        s.nextLine();
        System.out.print("Enter the current date in the format YYYYMMDD: ");
        String date = s.nextLine();

        Camera nikon = new Camera(memory, date);
        String input = "";

        do {
            printMenu();
            System.out.print("Select a menu choice: ");
            input = s.nextLine();
            switch (input) {
                case "1":
                    System.out.println("There are " + nikon.getNumPhotographs() + " photograph" +
                            (nikon.getNumPhotographs() != 1 ? "s " : " ") + "in the camera.");
                    break;

                case "2":
                    System.out.print("Enter the photo's size: ");
                    int size = s.nextInt();
                    s.nextLine();
                    boolean b = nikon.takePhoto(size);


                    if(b)
                        System.out.println(" (*) New photo added at position " + (nikon.getNumPhotographs()-1) +
                                " with size " + size);
                    else
                        System.out.println(" (!) There was a problem adding the photo.");
                    break;

                case "3":
                    System.out.println("Which of the following factories would you like:");

                    System.out.println("a) All 255's");
                    System.out.println("b) Minimum");
                    System.out.println("c) Checkerboard");

                    String abc = s.nextLine();
                    b=false;

                    switch (abc) {
                        case "a":
                            b = nikon.takePhoto(Photograph.createAll255sPhotograph());
                            break;

                        case "b":
                            b = nikon.takePhoto(Photograph.createMinimumPhotograph());

                            break;

                        case "c":
                            b = nikon.takePhoto(Photograph.create7x7Checkerboard());
                            break;
                    }

                    if(b)
                        System.out.println(" (*) New photo added at position " + (nikon.getNumPhotographs()-1) +
                                " with size " + nikon.getPhotoSize(nikon.getNumPhotographs()-1));
                    else
                        System.out.println(" (!) There was a problem adding the photo.");

                    break;

                case "4":
                    System.out.print("Enter the camera's date: ");
                    String d = s.nextLine();
                    nikon.setDate(d);
                    break;

                case "5":
                    findOldestPhotograph(nikon);
                    break;

                case "6":
                    findLargestPhotograph(nikon);
                    break;


                case "7":
                    showAllPhotos(nikon);
                    break;
            }

            System.out.println();
        } while (!input.equalsIgnoreCase("Q"));
    }

    public static void printMenu() {
        System.out.println("1) Display the number of photographs");
        System.out.println("2) Add a randomly-generated photograph");
        System.out.println("3) Add a factory-generated photograph");
        System.out.println("4) Set the camera's date");
        System.out.println("5) Display the date and size of the oldest photograph");
        System.out.println("6) Display the date and size of the largest photograph");
        System.out.println("7) Display the date and size of photographs in the camera's memory");
        System.out.println("Q) Halt the application");
    }

    public static void findLargestPhotograph(Camera c) {
        int largest = c.getPhotoSize(0);
        int idx = 0;

        for(int i=0; i<c.getNumPhotographs(); i++) {
            if(largest < c.getPhotoSize(i)){
                largest = c.getPhotoSize(i);
                idx = i;
            }

        }

        System.out.println("The largest photo size is " + largest + " and it was taken on " + c.getPhotoDate(idx));


    }

    public  static void findOldestPhotograph(Camera c) {
        String oldest = c.getPhotoDate(0);
        int idx = 0;

        for(int i=0; i<c.getNumPhotographs(); i++) {
            if(oldest.compareTo(c.getPhotoDate(i)) > 0){ //oldest appears after current, so no longer oldest
                oldest = c.getPhotoDate(i);
                idx = i;
            }

        }

        System.out.println("The oldest photo date was taken on " + oldest + " and it has size " + c.getPhotoSize(idx));


    }

    public static void showAllPhotos(Camera c) {
        for(int i=0; i<c.getNumPhotographs(); i++) {
            System.out.println(" [" + i + "] date: " + c.getPhotoDate(i) + " size: " + c.getPhotoSize(i));
        }
    }
}
