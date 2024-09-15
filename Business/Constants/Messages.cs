using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araç eklendi";
        public static string CarNameInvalid = "Araç ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string CarsListed = "Araçlar listelendi";
        public static string CarDeleted="Araç silindi";
        public static string CarUpdated = "Araç güncellendi";
        public static string ColorAdded = "Renk eklendi";
        public static string ColorDeleted = "Renk silindi";
        public static string ColorUpdated = "Renk güncellendi";
        public static string ColorsListed = "Renkler listelendi";
        public static string ColorNameInvalid = "Renk ismi geçersiz";
        public static string BrandAdded = "Marka eklendi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandUpdated = "Marka güncellendi";
        public static string BrandsListed = "Markalar listelendi";
        public static string BrandNameInvalid = "Marka ismi geçersiz";
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string CustomerUpdated = "Müşteri güncellendi";
        public static string CustomersListed = "Müşteriler listelendi";
        public static string CustomerNameInvalid = "Müşteri ismi geçersiz";
        public static string RentalDeleted = "Kiralama silindi";

        public static string RentalListed = "Kiralamalar listelendi";
        public static string RentalUpdated = "Kiralama güncellendi";
        public static string RentalAdded = "Kiralama eklendi";
        public static string CarInvalid = "Araç uygun değildir";

        public static string CarCountOfBrandError = "Bu kategorideki araç sayısı aşılmıştır";
        public static string CarNameAlreadyExists = "Bu isimde başka bir ürün bulunuyor.";
        public static string BrandLimitExceeded = "Marka limitine ulaşıldı";
		public static string? AuthorizationDenied = "Yetkiniz bulunmamaktadır";

		public static string UserRegistered = "Kayıt olundu";
		public static string UserNotFound = "Kullanıcı bulunamadı";
		public static string PasswordError = "Parola hatası";
		public static string SuccessfulLogin = "Başarılı giriş";

		public static string UserAlreadyExists = "Böyle bir kullanıcı zaten var";
		public static string AccessTokenCreated = "Access token yaratıldı";

        public static string CarUnavailable = "Araç uygun değil.";
        public static string CarAvailable = "Araç uygun";
    }
}
