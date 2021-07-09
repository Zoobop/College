# File Reader

# ******************** Functions ************************ #

def firstNameFromFile():
    # Get data from file
    file = open("FirstNames.txt", "r", )
    
    # Parse data into list
    names = []
    for line in file:
        name = line[0:len(line) - 1]
        names.append(name)

    # Close file
    file.close()

    return names

def lastNameFromFile():
    # Get data from file
    file = open("LastNames.txt", "r")
    
    # Parse data into list
    names = []
    for line in file:
        name = line[0:len(line) - 1]
        names.append(name)

    # Close file
    file.close()

    return names