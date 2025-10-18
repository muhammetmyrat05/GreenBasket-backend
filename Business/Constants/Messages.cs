using System;

namespace Business.Constants
{
    public static class Messages
    {
        // Genel
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserNotAuthenticated = "Lütfen giriş yapın.";
        public static string OperationSuccessful = "İşlem başarıyla tamamlandı.";
        public static string OperationFailed = "İşlem başarısız.";

        // Kullanıcı
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string UserUpdated = "Kullanıcı bilgileri güncellendi.";
        public static string UserRoleChanged = "Kullanıcı rolü değiştirildi.";

        // Tarifler
        public static string RecipeAdded = "Tarif başarıyla eklendi.";
        public static string RecipeDeleted = "Tarif silindi.";
        public static string RecipeUpdated = "Tarif güncellendi.";
        public static string RecipesListed = "Tarifler listelendi.";
        public static string RecipeNotFound = "Tarif bulunamadı.";

        // Market / Ürün
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductDeleted = "Ürün silindi.";
        public static string ProductUpdated = "Ürün güncellendi.";
        public static string ProductNotFound = "Ürün bulunamadı.";

        // Siparişler
        public static string OrderCreated = "Sipariş oluşturuldu.";
        public static string OrderCancelled = "Sipariş iptal edildi.";
        public static string OrderCompleted = "Sipariş tamamlandı.";

        // Sistem
        public static string MaintenanceTime = "Sistem bakımda, lütfen daha sonra tekrar deneyin.";
    }
}
