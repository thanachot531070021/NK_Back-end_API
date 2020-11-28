using SimplePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NK_Back_end_API.Models
{
    public class PasswordHashModel
    {
        //Hash Password Method
        //PasswordHashModel.Hash(model.password)
        public static string Hash(string password) 
        {
            var saltedPasswordHash = new SaltedPasswordHash(password,20);
            return saltedPasswordHash.Hash +":"+ saltedPasswordHash.Salt;

        }


        //Verify  Password Method
        //verify = PasswordHashModel.Verify("111111", model.password)

        public static bool Verify(string password,string passwordHash)
        {
            string[] passwordHashes = passwordHash.Split(':');
            if (passwordHashes.Length == 2) {
                var saltedPasswordHash = new SaltedPasswordHash(passwordHashes[0], passwordHashes[1]);
                return saltedPasswordHash.Verify(password);
            }
                return false;

        }
    }
}