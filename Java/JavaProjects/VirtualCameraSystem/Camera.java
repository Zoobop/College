package com.company;

import java.util.ArrayList;
import java.util.Objects;

public class Camera {

    private ArrayList<Photograph> memory;
    private double zoom;
    private String date;

    public Camera() {
        memory = new ArrayList<Photograph>();
        zoom = 1.0;
        date = "19000101";
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Camera camera = (Camera) o;
        return Double.compare(camera.zoom, zoom) == 0 &&
                Objects.equals(memory, camera.memory) &&
                Objects.equals(date, camera.date);
    }

    public Camera(int num, String date) {
        if(num < 16)
            num = 16;

        memory = new ArrayList<Photograph>(num);

        zoom = 1.0;

        this.date = date;
    }

    public String getDate() {
        return date;
    }

    public double getZoom() {
        return zoom;
    }

    public void setDate(String date) {
        if(date.length() == 8)
            this.date = date;
    }

    public void setZoom(double zoom) {
        if(zoom >= 0 && zoom <= 1)
            this.zoom = zoom;
    }

    public boolean takePhoto(Photograph p) {
        memory.add(p);
        return true;
    }

    public boolean takePhoto(int num) {
        Photograph in_p = new Photograph(num, this.getDate());

        for(int i=0; i < in_p.getSize(); i++) {
            in_p.setPixel(i, (int)(Math.random()*255) + 1); //(int) [0,1)*255 => (int) [0,255) => {0,1, ..., 254}
        }

        return takePhoto(in_p);
    }

    public int getPhotoSize(int idx) {
        if(idx >= 0 && idx < memory.size() && memory.get(idx) != null)
            return memory.get(idx).getSize();
        else
            return -1;
    }


    public String getPhotoDate(int idx) {
        if(idx >= 0 && idx < memory.size() && memory.get(idx) != null)
            return memory.get(idx).getDate();
        else
            return "00000000";
    }

    public int getNumPhotographs() {
        return 0;
    }
}