import numpy as num
import os
from numpy import linalg as LA

matrix = []
with open("origin_matrix.txt", 'r') as file:
    for line in file:
        matrix.append([float(x) for x in line.split()])

w_matrix = LA.eigvals(matrix)


if (os.path.exists("eigenvalues.txt")):
    os.remove("eigenvalues.txt")


with open("eigenvalues.txt", 'w') as uf:
    for i in range(len(w_matrix)):
        uf.write(str(w_matrix[i]))
        uf.write("\n")



