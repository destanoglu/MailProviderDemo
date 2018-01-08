# MailProviderDemo
MailProvider with MassTransit and Brighter Command Processor

Requirements:

Rest tabanlı light bir mail gateway tasarlayacağız. Bu mail gateway "OrderMail", "LostPasswordMail" ve "ShipmentMail" gibi farklı type'ları handle edebilmektedir ve ileride n* kadar daha farklı type'a da sahip olabileceği öngörülmektedir.

Bu mailler n* kadar farklı provider tarafından gönderilebilmektedir. Sistem şuan gönderim işlemini sadece "ABCProvider" ve "CDCProvider" firmaları ile gerçekleştirmektedir.

Bunlara ek olarak gateway'in kendisine gelen her bir request'i istenilen mail type'lara uygun olup olmadığını kontrol edebilecek akıllı validator'lara da ihtiyacı vardır.

Günün sonunda gateway'in async olarak çalışabilmesi istenmektedir ve aşağıdaki özellikleri barındırmalıdır: 

- Scheduled mail'ler gönderilebilmelidir
- Aşırı yük altında kolay bir şekilde scale edilebilir olmalıdır
- Yeni mail type'ları kolay bir şekilde eklenebilmelidir
- Failure anında belli bir interval ile fail veren mail'ı tekrardan retry edebilme yetisine sahip olmalıdır
- 3rd party firmalar kaynaklı yaşanacak transient failure tiplerinde kendisini korumaya alabilmelidir. 
