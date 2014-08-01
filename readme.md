Python/C# Encryption/Decryption Example
=======================================

The purpose of this project is to show an example of python encrypting a string, using RSA encryption, then decrypting that using C#.

As such, it includes 2 areas:
	- A python module area (./python)
	- A C# solution area (./csharp) - To be opened in Visual Studio

## Watch it work
The project should work out of the box, with a little setup:

### 1. Python Setup
```
cd python
virutalenv env
. ./env/bin/activate
pip install -r requirements.txt
```

### 2. C# Setup
Just open `./csharp/EncryptionDemos/EncryptionDemos.sln` in Visual Studio 2010 or later

### 3. Encrypt string with Python
```
cd python
. ./env/bin/activate
python encrypt.py
```
That will generate a file outside the python and c# areas called *encrypted.txt*.

### 4. Decrypt string using C#
1. From visual studio, run the project
2. Choose option 2

That should show you the original string from the encrypt.py script ("hello there!" is the default)

## Generating New Keys
The project comes with sample keys but if you want to create new ones, you can do so.

### Create initial keys
The keys are initially created in C#:

1. From visual studio, run the project
2. Choose option 1

### Put keys into the right places
That will create a *PrivateKey.txt* and *PublicKey.txt* at the root of this repository (outside both the Python and C# areas).

The *PrivateKey.txt* can be copied directly into the `./csharp/EncryptionDemos` directory.

For python, the `PublicKey.txt` string needs to be converted into PEM format. You can use [this utility from SuperDry Developer](https://superdry.apphb.com/tools/online-rsa-key-converter) to do that conversion. Once you  have it, paste it into `./python/publickey.pem`.
