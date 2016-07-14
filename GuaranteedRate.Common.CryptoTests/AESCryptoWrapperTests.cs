using NUnit.Framework;
using System;
using System.Linq;

namespace GuaranteedRate.Common.Crypto.Tests
{
    [TestFixture()]
    public class AESCryptoWrapperTests
    {

         

        private void TestIt(string plaintext, string key)
        {
            var enc = new AESCryptoWrapper();
            var ciphertext = enc.Encrypt(plaintext, key);
            var plainAgain = enc.Decrypt(ciphertext, key);
            Assert.That(plainAgain == plaintext, $"expected {plaintext}, got {plainAgain}");


        }

        [Test]
        public void KeyProviderTest()
        {
            var keyProvider = new ConfigKeyProvider();
            Assert.That(keyProvider.GetKeyAsBase64String("my-key-name") == "OSHv4XN4Jr+eaDaOtBDYbWa9NY+6x1+VKgxatoa3d/o=");

        }

        [Test()]
        public void ByteTests()
        {
            var enc = new AESCryptoWrapper();
            var key = Convert.ToBase64String(enc.GenerateKey());
            var plaintext = new byte[256];
            new Random().NextBytes(plaintext);

            var iv = new byte[16];

            var result = enc.Encrypt(plaintext, Convert.FromBase64String(key), iv);

            Assert.That(result != plaintext);
            var decrypted = enc.Decrypt(result, Convert.FromBase64String(key));
            Assert.That(plaintext.SequenceEqual<byte>(decrypted));
        }

        [Test()]
        public void SimpleTextTests()
        {
            var enc = new AESCryptoWrapper();
            var key = Convert.ToBase64String(enc.GenerateKey());
           
           
            var strings = new[]
            {
                @" Roof party bicycle rights kale chips chicharrones, squid swag hashtag. Cardigan ethical migas seitan gentrify, williamsburg blue bottle green juice taxidermy next level occupy. Wolf next level keffiyeh meditation, green juice beard chia skateboard normcore irony bicycle rights VHS fixie leggings forage. Direct trade fixie slow-carb lo-fi art party, gochujang photo booth. Locavore venmo four loko, jean shorts sartorial meditation mlkshk taxidermy fixie bushwick XOXO authentic gastropub. Pug cred vinyl fashion axe XOXO ennui. Actually selfies flannel cold-pressed austin.",
                "9/1/1991",
                "dv$XhkIEG#GW5@pUpvrgibG%i6c''\"",
                "덕분에 제 인생을 바꿨어요",
                "የኔ ማንዣበቢያ መኪና በዓሣዎች ተሞልቷል",
                "Моё судно на воздушной подушке полно угрей",
                "Mi aerodeslizador está lleno de anguilas",
                "مېنىڭ ھاۋا-نېگىز كېمەمدە يىلان بېلىق تۇشۇپ كەتتى",
                "第十八章 第十六章 第十四章. .伯母さん 復讐者」. 第十一章 第十八章 第十六章.復讐者」. 第十一章 第十三章 第十七章 第十五章. 第十四章 第十一章 第十七章 第十九章 第十八章 第十五章. 復讐者」. 第九章 第五章 第四章.第三章 第六章 第七章 第八章. 伯母さん 復讐者」.",
                "the equals sign is used for padding sometimes ===",
                "the equals sign is used for padding sometimes =======================",
                "the equals sign is used for padding sometimes=",
                "the equals sign is used for padding sometimes ======",
                "Ове Као међ језераца метеризи положене лед има упорство ред. Већ moschatus Сви сва Dwellings giganteum муљ. Ursus Bison alces ~ПРВОБИТНА Jolly Eleph Munro. Сл те би лице сиге Сохе сати тиме на То за веку. Рипе Снег лек међ код Доњи већ виле итд. Употреба Но кватерно кречњака друкчија им Он Ми. Пак megaceros има Hipparion бар moschatus amphibius giganteum europaeus све. Hipparion spelea giganteum нам Dwellings moschatus још ова ову. Ursus дрвета дрвено alces Jolly Eleph Munro шљунка судова Bison. Коњ Ако ватра“ „Усред чак eximia или. "  
            };

            foreach (var s in strings)
            {
                TestIt(s, key);
            }


        }




    }
}