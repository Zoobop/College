# Bank Account Example

import os
import time
import datetime
import BankDatabase

# ******************** Functions ************************ #

# Adds the account to the dictionary of accounts
def addToListing(account):
    if (isinstance(account, BankDatabase.BankAccount)):
        listOfAccounts[account.accountID] = account
    return


# Retrieves the Account data from database
def retrieveAccountListing():
    if (type([]) is list):
        for account in BankDatabase.accountListing:
            addToListing(account)
    return


# Returns a number as a string formatted by the number of accounts in listing (i.e #009 if the number of accounts is 3 digits long)
def formatAccountIndex(index):

    # Get number of characters in list
    numOfCharsInList = 0
    for _ in str(len(listOfAccounts)):
        numOfCharsInList += 1

    # Get number of characters in index
    numOfCharsInIndex = 0
    for _ in str(index):
        numOfCharsInIndex += 1

    # Get number of zeros to add
    numOfZerosToAdd = numOfCharsInList - numOfCharsInIndex

    # Format index -- Add zeros to index
    formattedIndex = ""
    for i in range(1, numOfZerosToAdd + 1):
        formattedIndex += '0'

    return formattedIndex + str(index)


# Clear screen
def clear():
    os.system('cls')


# Display account information for all accounts in the database
def displayAccounts():
    formatText = "#{}\t Account ID: {}\t - Routing Number: {}\t - Funds: ${:.2f}\t - Owner: {}"

    i = 1
    for id, account in listOfAccounts.items():
        # Format index
        i = formatAccountIndex(i)

        print(formatText.format(i, id, account.routingNumber, account.money, (account.lastName + ", " + account.firstName)))
        i = int(i) + 1
    print()
    return


# Asks the user for account ID to log in
def accountLogin():
    correctID = False
    while (correctID == False):
        userInput = input("Enter Account ID: ")

        if (userInput.isnumeric()):
            userInput = int(userInput)
            for ID in listOfAccounts.keys():
                if (userInput == ID):
                    correctID = True
                    break

        if (correctID):
            print("\nLogging Into Your Account!")
            time.sleep(2)
            accountManagement(listOfAccounts[userInput])
    return


# Displays the main selection screen when logged in
def accountDisplay(account):
    print("**************************************")
    print("Welcome To Your Account, {}".format(account.firstName))
    print("**************************************")
    print(datetime.datetime.now())
    print()
    return


# Allows the depositing/withdrawl of an account
def accountManagement(account):
    isLoggedIn = True
    while (isLoggedIn):
        clear()

        # Pretty display design
        accountDisplay(account)
        print("(0) Check Balance")
        print("(1) Deposit Funds")
        print("(2) Withdrawl Funds")
        print("(3) Log Out")
        print()

        userInput = input("Selection: ")
        if (userInput.isdigit()): userInput = int(userInput)

        # Validate input
        if (userInput == 0): checkBalance(account)
        elif (userInput == 1): depositFunds(account)
        elif (userInput == 2): withdrawFunds(account)
        elif (userInput == 3): isLoggedIn = False


# Displays the current funds of the account
def checkBalance(account):
    clear()
    accountDisplay(account)

    # Display funds
    print("Account Funds:\t${:.2f}".format(account.money))
    print()

    input("Press any key to return: ")
    return


# Allows the user to deposit money into the account
def depositFunds(account):
    hasFinished = False
    while (hasFinished == False):
        clear()
        accountDisplay(account)

        # Validate input
        userDeposit = input("Deposit Amount (Enter \'C\' to Cancel Transaction): ")
        if (userDeposit.isnumeric()):
            print()
            account.deposit(float(userDeposit))
            time.sleep(2)
            hasFinished = True
        elif (userDeposit.lower == 'c'):
            hasFinished = True
    return


# Allows the user to withdrawl money from the account
def withdrawFunds(account):
    hasFinished = False
    while (hasFinished == False):
        clear()
        accountDisplay(account)

        # Display funds
        print("Account Funds:\t${:.2f}".format(account.money))
        print()

        # Validate input
        userWithdrawl = input("Withdrawl Amount (Enter \'C\' to Cancel Transaction): ")
        if (userWithdrawl.isnumeric()):
            print()
            account.deposit(float(userWithdrawl))
            time.sleep(2)
            hasFinished = True
        elif (userWithdrawl.lower == 'c'):
            hasFinished = True
    return


# ******************** Program ************************ #

# Accounts to display
listOfAccounts = { }

# Retrieve accounts from bank database
retrieveAccountListing()

# Displays the each account
displayAccounts()

# Account Management System
inProgram = True
while (inProgram):
    accountLogin()
