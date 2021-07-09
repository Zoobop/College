package com.company;

public class ColorPhotograph extends Photograph {

    public ColorPhotograph(int size, String date) {
        super(size, date);
    }

    public int encodePixel(int red, int green, int blue) {
        if( red >= 0 && green >= 0 && blue >= 0 &&
                red <= 255 && green <= 255 && blue <= 255) {
            green = green << 8;
            blue = blue << 16;
            return blue | green | red;
        }
        else
            return 0;
    }

    public void setPixel(int index, int red, int green, int blue) {
        setPixel(encodePixel(red, green, blue), index);
    }

    @Override
    public void setPixel(int value, int index) {
        super.setPixel(value, index);
    }
}