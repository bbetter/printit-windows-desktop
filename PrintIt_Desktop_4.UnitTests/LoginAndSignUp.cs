using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Enums;

namespace PrintIt_Desktop_4.UnitTests
{
    [TestClass]
    public class LoginAndSignUp
    {
        
        [TestMethod]
        public void LoginValidation()
        {
            var validator = Config.Validation.GetLoginValidator();
            var valid = LoginValidationState.Valid;
            var invalid = LoginValidationState.NotEMail;
            Assert.AreEqual(validator.Validate(@"unnamed999999999@gmail.com")[0], valid);
            Assert.AreEqual(validator.Validate(@"Unnamed999999999@Gmail.com")[0], valid);
            Assert.AreEqual(validator.Validate(@"Un.named999999999@Gmail.com")[0], valid);
            Assert.AreEqual(validator.Validate(@"1mail@domain.com")[0], valid);
            Assert.AreEqual(validator.Validate(@"a.b@c.d")[0], invalid);
            Assert.AreEqual(validator.Validate(@"mail")[0], invalid);
            Assert.AreEqual(validator.Validate(@"google.com.ua")[0], invalid);
        }

        [TestMethod]
        public void PasswordValidation()
        {
            var validator = Config.Validation.GetPasswordValidator();
            var tooLong = PasswordValidationState.TooLong;
            var tooShort = PasswordValidationState.TooShort;
            var valid = PasswordValidationState.Valid;
            Assert.AreEqual(validator.Validate(@"12345")[0], valid);
            Assert.AreEqual(validator.Validate(@"123456")[0], valid);
            Assert.AreEqual(validator.Validate(@"1234567890123456789012345")[0], valid);
            Assert.AreEqual(validator.Validate(@"1234")[0], tooShort);
            Assert.AreEqual(validator.Validate(@"12345678901234567890123456")[0], tooLong);
        }
    }
}
