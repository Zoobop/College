from .models import User, db

txt_file_name = "account_registry.txt"

def generate_file():
    file = open("website/" + txt_file_name, "w")
    for objects in db.session.identity_map:
        file.write(objects)
    file.close()
    print("Exported Data to Text!")

def add_to_file(user: User):
    file = open("website/" + txt_file_name, "a")
    file.write(f"{user.id} {user.email} {user.password} {user.first_name}\n")
    file.close()