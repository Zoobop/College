// Brandon Cunningham
// C00431388
// CMPS 261
// Program Description: description of actions of code
// Certificate of Authenticity:
// I certify that the code in the method functions
// including method function main of this project are
// entirely my own work.

package com.company;

import listapi.*;

public class pa4_2_C00431388 {
    public static void main(String[] args) {
        GenericQueue<Integer> queue = new GenericQueue<Integer>();
        GenericStack<Integer> stack = new GenericStack<Integer>();
        MyArrayList<Integer> list = new MyArrayList<Integer>();

        System.out.println("Enqueue");
        for (int i = 1; i <= 5; i++) {
            queue.enqueue(i);
            System.out.println("(" + i + ") " + queue);
        }

        System.out.println("\nDequeue & Add To Stack");
        for (int i = 1; i <= 5; i++) {
            stack.push(queue.dequeue());
            System.out.println("(" + i + ") " + queue + " " + stack);
        }

        System.out.println("\nPop & Add To List");
        for (int i = 1; i <= 5; i++) {
            list.add(stack.pop());
            System.out.println("(" + i + ") " + stack + " List: " + list);
        }
    }
}
