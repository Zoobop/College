# Name: Brandon Cunningham
# ULID: C00431388
# Course: CMPS 315 - Fall 2022
# Assignment: pa2 - Password Cracking
#
# Certification of Authenticity:
# I certify that this assignment is entirely my own work


import hashlib

# Retrieves the data from the file
def get_data_from_file(path: str):
    data = []
    file = open(path, 'r')
    for line in file.readlines():
        data.append(line.strip())       
    file.close()
    return data

# Parses the string into username and hash data
def parse_user_data(data: str) -> list[str]:
    parsed_data = { }
    for line in data:
        list = line.split(':', 2)
        username = list[0]
        password_data = list[1][slice(1, len(list[1]))].split('$')
        parsed_data[username] = password_data
    return parsed_data

# Cracks the hashes to get their original value
def find_matching_passwords(user_data: dict, passwords: list[str]) -> dict:
    matched_passwords = {}
    for username, password_data in user_data.items():
        salt = password_data[1]
        hash = password_data[2]
        for password in passwords:
            password_hex = hashlib.md5((salt + password).encode('utf-8')).hexdigest()
            # Checks the password hash against the stored hash
            if (hash == password_hex):
                matched_passwords[username] = password
                break
    return matched_passwords


# Displays the hash with their corresponding password
def display_hash_cracking(user: dict, matched_passwords: dict) -> None:
    print()
    print("Name     Salt    \t\t\t  Hash\t\t\t        Password")
    print("----------------------------------------------------------------")
    for username, password_data in user.items():
        matched_password = matched_passwords.get(username)
        matched_password = matched_password if matched_password != None else ""
        print(f"{username}   {password_data[1]}   {password_data[2]}   {matched_password}")
    print()
    print(f"Num hashes tested: {len(user)}")
    print(f"Cracked passwords: {len(matched_passwords)}")
    print(f"Unknown passwords: {len(user) - len(matched_passwords)}")


# Entry point
def main():
    # Get data fom files
    shadow_file_data = get_data_from_file("ShadowFile.txt")
    common_passwords = get_data_from_file("rockyouDictionary.txt")
    
    # Parse and retrieve targeted data
    user_map = parse_user_data(shadow_file_data)
    found_passwords = find_matching_passwords(user_map, common_passwords)
    
    # Display results
    display_hash_cracking(user_map, found_passwords)


if __name__ == '__main__':
    main()