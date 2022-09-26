import math
import time

questList = ["3 * 3", "5 ** 2", "8 * 5 + 5", "6 + 5 * 4", "4!"]
ansList = ["9", "25", "45", "26", "24"]
scoreList = []
inputList = []

def question():
    while len(inputList) != len(questList):
        qnum = (len(inputList) + 1)
        print("Question #" + str(qnum))
        print()
        x = input(questList[(qnum - 1)] + " = ")
        if x.isdigit():
            inputList.append(x)
            print()
        else:
            qnum = qnum - 1
            print("\nAnswer not an integer.\nTry Again!")
            print(type(x))
            print()
    return

def result():
    for i in questList:
        print("Question #" + str(questList.index(i) + 1))
        print()
        print(i + " =", ansList[questList.index(i)])
        print()
        if inputList[questList.index(i)] == ansList[questList.index(i)]:
            x = ("")
            scoreList.append(1)
        else:
            x = ("X")
        print("Your Answer:", inputList[questList.index(i)], x)
        print("\n" * 2)
    return


print("****************Math Test v1.2****************\n\nQuestions only take integer values.\n")
name = input("Enter your name: ")
print()
print("**********************************************\n\nBeginning Test\n\n**********************************************\n")
time.sleep(2)
start = time.time()


question()


stop = time.time()
print("Calculating Answers...")
time.sleep(3)

seconds = (stop - start)
m = int(seconds // 60)
sec = round(seconds % 60)
print("\n" * 2)


print("*************Test Results*************")
print()
print("Name:", name)
print()


result()


score = len(scoreList)

print("Your Score:", score, "/ 5    ", ((score/5) * 100),"%")
print()
print("Time:", str(m) + "min", str(sec) + "sec")
print("\n" * 5)
