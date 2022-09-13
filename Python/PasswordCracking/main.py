# Name: Brandon Cunningham
# ULID: C00431388
# Course: CMPS 315 - Fall 2021
# Assignment: pa1 - Password Cracker
#
# Certification of Authenticity:
# I certify that this assignment is entirely my own work


import hashlib

# Retrieves the data from the file
def get_data_from_file(path: str):
    hash_data = []
    file = open(path, 'r')
    for hash in file.readlines():
        hash_data.append(hash.strip())
    file.close()
    return hash_data


# Cracks the hashes to get their original value
def crack_hashes(hashes, passwords):
    cracked = []
    stats = 0
    for hash in hashes:
        cracked_password = ""
        for password in passwords:
            password_hex = hashlib.md5(password.encode('utf-8')).hexdigest()
            # Checks the password hash against the stored hash
            if (hash == password_hex):
                cracked_password = password
                stats += 1
                break
        cracked.append([hash, cracked_password])
    return cracked, stats


# Displays the hash with their corresponding password
def display_hash_cracking(info):
    cracked_hashes = info[0]
    stats = info[1]

    print()
    print("\t\tHash\t\tCracked Password")
    print("--------------------------------------------------------")
    for pair in cracked_hashes:
        print(f"{pair[0]} --> {pair[1]}")
    print()
    print(f"Num hashes tested: {len(cracked_hashes)}")
    print(f"Cracked passwords: {stats}")
    print(f"Unknown passwords: {len(cracked_hashes) - stats}")


# Entry point
def main():
    file_path = input("Enter name of the hash file: ")
    hashes = get_data_from_file(file_path)
    passwords = get_data_from_file("rockyou_500Thousand.txt")
    display_hash_cracking(crack_hashes(hashes, passwords))


if __name__ == '__main__':
    main()