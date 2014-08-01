#! env/bin/python
import os

from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_v1_5

BASE_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
ENCRYPTED_STRING_PATH = os.path.join(BASE_DIR, "encrypted.txt")

if __name__ == "__main__":
    # Read key from file
    key_text = open("publickey.pem", "r").read()

    # Import key using RSA module
    public_key = RSA.importKey(key_text)

    # Generate a cypher using the PKCS1.5 standard
    cipher = PKCS1_v1_5.new(public_key)

    # Encrypy as bytes
    encrypted_bytes = cipher.encrypt("hello there!")

    # Write encrypted string to file
    print "Writing encrypted string to %s..." % ENCRYPTED_STRING_PATH
    with open(ENCRYPTED_STRING_PATH, "wb") as f:
        f.write(encrypted_bytes.encode("base64"))
