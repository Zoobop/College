package com.company;

public class Photograph {

    private int[] pixelArray;
    private String date = "01011900";

    public Photograph() {
        pixelArray = new int[100];
    }

    public Photograph(int size, String date) {
        if (size >= 4)
            pixelArray = new int[size];
        else
            pixelArray = new int[4];

        if (date.length() == 8)
            this.date = date;
        else
            this.date = "01011900";
    }

    public void setPixel(int location, int value) {
        if (location >= 0 && location <= pixelArray.length && value >= 0 && value <= 255)
            pixelArray[location] = value;
    }

    public String getDate() {
        return date;
    }

    public int getSize() {
        return pixelArray.length;
    }

    public static Photograph createAll255sPhotograph() {
        Photograph p = new Photograph(100, "19000101");
        for (int i = 0; i < p.pixelArray.length; i++) {
            p.pixelArray[i] = 255;
        }

        return p;
    }

    public static Photograph createMinimumPhotograph() {
        return new Photograph(4, "19000102");
    }

    public static Photograph create7x7Checkerboard() {
        Photograph p = new Photograph(49, "19000103");
        for (int i = 0; i < p.pixelArray.length; i++) {
            if (i % 2 == 0)
                p.pixelArray[i] = 255;
            else
                p.pixelArray[i] = 0;
        }

        return p;
    }
}