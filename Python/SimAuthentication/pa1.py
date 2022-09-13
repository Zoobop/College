import hashlib
import hmac
import random

server_data_path = "SampleFiles/ServerData.txt"
sim_data_path = "SampleFiles/SimData.txt"

# FUNCTIONS
def get_data(path: str):
    data = []
    with open(file=path, mode="r") as file:
        lines = file.readlines()
        for line in lines:
            
            if (line.isspace()):
                continue
            
            pair = line.split(" ")
            ismi, key = pair[0], pair[1].removesuffix("\n")
            data.append((ismi, key))

    return dict(data)

# Get file data
server_data = get_data(server_data_path)
sim_data = get_data(sim_data_path)

def error_result(imsi: str):
    print(f"ERROR: Invalid IMSI ({imsi})\n")

def generate_results(imsi: str, key: str):
    print(f"SIM IMSI: {imsi}\n")
    
    # Calculate
    if (server_data.get(imsi) == None):
        error_result(imsi=imsi)
        return
    
    server_key = str(server_data.get(imsi))
    random_number = random.randrange(1000, 10000)
    sim_hash_result = hmac.new(key=key.encode(), msg=str(random_number).encode(), digestmod=hashlib.md5).hexdigest()
    server_hash_result = hmac.new(key=server_key.encode(), msg=str(random_number).encode(), digestmod=hashlib.md5).hexdigest()
    
    # Display
    print("Challenge Calculations")
    print("----------------------")
    print(f"Server secret:  {server_key}")
    print(f"Random number:  {random_number}")
    print(f"Challenge hash: {server_hash_result}\n")
    
    print("Response Calculations")
    print("----------------------")
    print(f"SIM secret:     {key}")
    print(f"Random number:  {random_number}")
    print(f"Response hash:  {sim_hash_result}\n")
    
    if (server_hash_result == sim_hash_result):
        print("Successful Authentication: Access GRANTED\n")
    else:
        print("Authentication Failure: Access DENIED\n")
    
# Main
def main():

    # Sample inputs
    sim_imsi = list(sim_data)[0] # First ismi from SimData.txt file (index reflects line order)
    sim_key = sim_data.get(sim_imsi) # Gets the secret key
    
    # Display
    generate_results(imsi=sim_imsi, key=sim_key)
    
    # UNCOMMENT CODE TO VIEW A SAMPLE RUN THROUGH
    '''
    input_count = len(sim_data)
    for i in range(input_count):
        # Sample inputs
        sim_imsi = list(sim_data)[i]
        sim_key = sim_data.get(sim_imsi)
        
        # Display
        print(f"Sample Run #{i+1}")
        generate_results(imsi=sim_imsi, key=sim_key)
    '''
    
if __name__ == '__main__':
    main()