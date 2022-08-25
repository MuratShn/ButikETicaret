# ButikETicaret
Bir Butik Mini E-Ticaret uygulamasıdır 
Özellik olarak klasik rollere göre girişler mevcuttur(2 adet satıcı ve kullanıcı)
kullanıcı rölü  ürün sipariş'i ,favorilere ekleme ,sipariş ettiği ürüne yorum yapma , sepete ekleme , klasik kullanıcı ayarları yapılandırma vb işlemleri yaparken <br/>
satıcı rölü kullanıcıdan farklı olarak ürün ekleyip düzenleyebilmekte <br/>
Proje hedeflediğimden çok daha uzun sürdü bunun en büyük sebeplerinden biri ürün varyantı olması yani bir üründe hem renkler hem bedenler olunca karışıklık meydana geliyor <br/>
Çok fazla eksik kısmı olduğunun farkındayım zamanla onlarıda kapatmayı hedefliyorum

<br/>
<br/>

-- Temel olarak kullandıgım teknolojiler/kütüphaneler --
Klasik Katmanlı Mimari ,<br />
EntityFramework(Data Access Kısmında Bolca Kullanıldı), <br />
Migration(Microsoft Identiy için kullanmak durumda kaldım diğer tabloları kendim oluşturmuştum), <br />
Fluent Validation (Çoğu modellere uygulanmadı) , <br />
.Net Core Identity (RefreshPassword,RefreshToken,ExternalLogin(Google,Facebook)...), <br />
JWT Token(Rol bazlı çalışma mevcuttur),  <br />
Microsoft Cache (1 2 yerde kullanıldı sadece) <br />
CustomResult yapısı oluşturulup her yerde o kullanılmıştır, <br />
....

<br/>
<br/>
