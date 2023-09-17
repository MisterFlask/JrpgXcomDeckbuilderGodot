import os

def main():
    # Get the directory where the script file resides
    project_directory = os.path.dirname(os.path.abspath(__file__))

    # Task 1: Remove all files with the .meta extension
    for root, dirs, files in os.walk(project_directory):
        for file in files:
            if file.endswith(".meta"):
                os.remove(os.path.join(root, file))

    # Task 2: For each .cs file, remove every line that includes the string "using UnityEngine;"
    for root, dirs, files in os.walk(project_directory):
        for file in files:
            if file.endswith(".cs"):  # Limiting the operation to .cs files
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, 'r', encoding='utf-8') as f:
                        lines = f.readlines()
                
                    with open(file_path, 'w', encoding='utf-8') as f:
                        for line in lines:
                            if "using UnityEngine;" not in line:
                                f.write(line)
                except UnicodeDecodeError:
                    print(f"Failed to process file (UnicodeDecodeError): {file_path}")

if __name__ == "__main__":
    main()
