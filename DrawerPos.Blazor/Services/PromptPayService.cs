using Saladpuk.PromptPay;
using Saladpuk.PromptPay.Facades;
using System;

namespace DrawerPos.Blazor.Services
{
    public class PromptPayService
    {
        public string GenerateBankAccountQRCode(string metthodpayment, double amount, string merchant)
        {
            // Generate PromptPay QR code
            string qrCode = PPay.DynamicQR
                                .BankAccount(metthodpayment) // Use the mobile number provided
                                .Amount(amount) // Use the amount provided
                                .BillerSuffix(merchant) // Use the merchant
                                .CreateCreditTransferQrCode(); // Create the QR code for credit transfer

            return qrCode;
        }
        public string GenerateNationalIdQRCode(string metthodpayment, double amount, string merchant)
        {
            // Generate PromptPay QR code
            string qrCode = PPay.DynamicQR
                                .NationalId(metthodpayment) // Use the mobile number provided
                                .Amount(amount) // Use the amount provided
                                .BillerSuffix(merchant) // Use the merchant
                                .CreateCreditTransferQrCode(); // Create the QR code for credit transfer

            return qrCode;
        }
        public string GenerateMobileNumberQRCode(string metthodpayment, double amount, string merchant)
        {
            // Generate PromptPay QR code
            string qrCode = PPay.DynamicQR
                                .MobileNumber(metthodpayment) // Use the mobile number provided
                                .Amount(amount) // Use the amount provided
                                .BillerSuffix(merchant) // Use the merchant
                                .CreateCreditTransferQrCode(); // Create the QR code for credit transfer

            return qrCode;
        }
        public string GenerateEWalletQRCode(string metthodpayment, double amount, string merchant)
        {
            // Generate PromptPay QR code
            string qrCode = PPay.DynamicQR
                                .EWallet(metthodpayment) // Use the mobile number provided
                                .Amount(amount) // Use the amount provided
                                .BillerSuffix(merchant) // Use the merchant provided
                                .CreateCreditTransferQrCode(); // Create the QR code for credit transfer

            return qrCode;
        }
    }
}
