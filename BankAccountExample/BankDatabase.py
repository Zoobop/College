# Bank Database

import random
import sys
import FileReader

# ******************** Classes ************************ #

# Bank Account class
class BankAccount:

    # Constructor
    def __init__(self, firstName, lastName, money, accountID, routingNumber):
        self.self = self
        self.firstName = firstName
        self.lastName = lastName
        self.money = money
        self.accountID = accountID
        self.routingNumber = routingNumber


    # Deposit money into account
    def deposit(self, amount):
        self.money += amount
        print("\nDepositing ${:.2f} into your account...".format(amount))
        return


    # Withdraw money from account
    def withdraw(self, amount):
        if (amount > self.money):
            print("Error in withdrawl: Exceeds current funds amount of ${:.2f}".format(self.money))
            return None
        else:
            print("Recieving ${:.2f}".format(amount))
            return amount

# ******************** Functions ************************ #

# Randomize account id and routing number
def generateAccountNumbers():
    return random.randint(1000000, sys.maxsize)


# Randomize funds in account
def generateFunds():
    return random.random() * float(random.randint(10, 100000))


# Generate a sample list of accounts
def autoGenerateSampleAccountListing(numberOfAccounts):
    firstNames = FileReader.firstNameFromFile()
    lastNames = FileReader.lastNameFromFile()

    if (len(firstNames) == 0 or len(lastNames) == 0):
        print("Error in data collection!")
        return

    for i in range(0, numberOfAccounts):
        randomFirst = firstNames[random.randint(0, len(firstNames) - 1)]
        randomLast = lastNames[random.randint(0, len(lastNames) - 1)]

        newAccount = BankAccount(randomFirst, randomLast, generateFunds(), generateAccountNumbers(), generateAccountNumbers())
        accountListing.append(newAccount)
    return

# ******************** Setup ************************ #

# List of accounts in database
accountListing = []


# Fill account listing 
autoGenerateSampleAccountListing(5000)
