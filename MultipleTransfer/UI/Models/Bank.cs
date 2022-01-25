using System;
namespace MultipleTransfer.UI.Models
{
    public class Bank
    {
        public string bankId { get; set; }
        public string bankName  { get; set; }
        public int mPhotoID { get; set; }
     
    }


    public class BankInstance
    {

        public Bank[] beneficiaries;

        static Bank[] benList =
        {
             new Bank() { bankId = "1234", mPhotoID = Resource.Drawable.sterling, bankName = "Sterling Bank" },
             new Bank() { bankId = "1234", mPhotoID = Resource.Drawable.wema, bankName = "Wema Bank" },
             new Bank() { bankId = "7890", mPhotoID = Resource.Drawable.gtb, bankName = "GTBank" },
             new Bank() { bankId = "89004", mPhotoID = Resource.Drawable.zenith, bankName = "Zenith" },
             new Bank() { bankId = "3455", mPhotoID = Resource.Drawable.union, bankName = "Union Bank" },
        };



        public BankInstance()
        {
            this.beneficiaries = benList;
        }

        public Bank this[int i]
        {
            get { return beneficiaries[i]; }
        }
    }


}
