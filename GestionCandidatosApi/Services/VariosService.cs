using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;
using BCrypt.Net;

namespace GestionCandidatosApi.Services
{
    public class VariosService
    {

        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("ememememem ememememe");
        private static readonly int Iterations = 1000; // Puedes aumentar el número de iteraciones

        public static string EncryptPassword(string plainPassword)
        {
            // El segundo parámetro es el número de rondas de salt (cuantas más, mejor seguridad pero más lento)
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, 12);
        }

        // Este método verifica si una contraseña sin cifrar coincide con el hash almacenado
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        // Este método verifica si la nueva contraseña es la misma que la antigua
        public static bool IsSamePassword(string newPlainPassword, string oldHashedPassword)
        {
            return VerifyPassword(newPlainPassword, oldHashedPassword);
        }
    }
}
