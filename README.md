
# Тема - автоматизация бронирования в отеле

Выполнила - Малышева Александра Юрьевна гр.ИП 20-3
ㅤ
ㅤ
ㅤ

#### Пример бизнес сценария - информационный чек


![image](https://github.com/malbled/BookingRoom/assets/107112651/668a6d3e-f315-4a9f-827b-75f5248ce63e)

Схема моделей
---

```mermaid
    classDiagram
    Booking <.. Room
    Booking <.. Hotel
    Booking <.. Guest
    Booking <.. Service
    Booking <.. Staff
    Staff .. Post

    class Room{
        +string Title
        +string TypeRoom
        +string? Description
    }
    class Hotel{
        +string Title
        +string Address
    }
    class Guest {
        +string LastName
        +string FirstName
        +string MiddleName
        +string Passport
        +string AddressRegistation
    }
    class Service {
        +string Title
        +string? Description
    }

    class Staff {
        +string LastName
        +string FirstName
        +string MiddleName
        +Post Post
    }
    class Booking {
        +Guid RoomId 
        +Guid HotelId
        +Guid ServiceId
        +Guid GuestId
        +Guid? StaffId
        +DateTimeOffset DateCheckIn
        +DateTimeOffset DateCheckOut
        +decimal Price
    }
    class Post {
        <<enumeration>>
        Manager(Менеджер)
        Administrator(Администатор)
        None(Неизвестно)
    }
```

SQL
---
```
INSERT INTO [dbo].[Guests]
           ([Id]
           ,[LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[Passport]
           ,[AddressRegistration]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('a1d7a1e2-595b-4320-bda5-15b692d80a8d'
           ,N'Шаталова'
           ,N'Анастасися'
           ,N'Федоровна'
           ,'1256 8963'
           ,N'Токарева 8'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)

INSERT INTO [dbo].[Guests]
           ([Id]
           ,[LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[Passport]
           ,[AddressRegistration]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('a10c928f-9b94-4651-9563-1bdc8458aaf7'
           ,N'Кожанова'
           ,N'Полина'
           ,N'Алексеевна'
           ,'1256 6565'
           ,N'Токарева 12'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)


INSERT INTO [dbo].[Hotels]
           ([Id]
           ,[Title]
           ,[Address]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('c59168e7-3159-41f7-b375-f5d9cb8a6aec'
           ,'Hotel Lux Top'
           ,N'Новоможайская д 67'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)

INSERT INTO [dbo].[Hotels]
           ([Id]
           ,[Title]
           ,[Address]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('b6281743-ce00-47c0-b2f9-6527c751e1ec'
           ,N'Hotel Lux Super Cool'
           ,N'Ленина д 89, Спб'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)


INSERT INTO [dbo].[Rooms]
           ([Id]
           ,[Title]
           ,[TypeRoom]
           ,[Description]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('23e24338-25cb-4e14-b667-d23213e15846'
           ,'101B'
           ,N'Double Lux'
           ,N'есть фен, холодильник'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)


INSERT INTO [dbo].[Rooms]
           ([Id]
           ,[Title]
           ,[TypeRoom]
           ,[Description]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('6c519692-e954-4563-ac3b-f17b3cabe993'
           ,'207B'
           ,'Single Econom'
           ,N'есть фен, холодильник и одна кровать'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)


INSERT INTO [dbo].[Services]
           ([Id]
           ,[Title]
           ,[Description]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('89771d7c-fa73-486b-949e-7476f1a21d10'
           ,N'Завтрак шведский стол'
           ,N'Хорошие блюда'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)


INSERT INTO [dbo].[Services]
           ([Id]
           ,[Title]
           ,[Description]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('e38ceb94-ea7f-4f8b-a980-8ee8072e984b'
           ,N'Обед шведский стол'
           ,N'Хорошие блюда очень вкусные'
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)



INSERT INTO [dbo].[Staffs]
           ([Id]
           ,[LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[Post]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('f5b3ac6b-0e22-460b-9647-73f420d35745'
           ,N'Малышева'
           ,N'Александра'
           ,N'Юрьевна'
           ,1
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)

INSERT INTO [dbo].[Staffs]
           ([Id]
           ,[LastName]
           ,[FirstName]
           ,[MiddleName]
           ,[Post]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('478bbc00-ba43-4469-a6fb-9df48272e4cb'
           ,N'Кочетков'
           ,N'Денис'
           ,N'Александрович'
           ,0
           ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)




INSERT INTO [dbo].[Bookings]
           ([Id]
           ,[HotelId]
           ,[GuestId]
           ,[RoomId]
           ,[StaffId]
           ,[ServiceId]
           ,[DateCheckIn]
           ,[DateCheckout]
           ,[Price]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('1efa636f-4c7a-4b58-8bf8-0fd02e389469'
           ,'c59168e7-3159-41f7-b375-f5d9cb8a6aec'
           ,'a10c928f-9b94-4651-9563-1bdc8458aaf7'
           ,'23e24338-25cb-4e14-b667-d23213e15846'
           ,'f5b3ac6b-0e22-460b-9647-73f420d35745'
           ,'89771d7c-fa73-486b-949e-7476f1a21d10'
           ,GETDATE()
           ,GETDATE()
           ,25000
            ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)


INSERT INTO [dbo].[Bookings]
           ([Id]
           ,[HotelId]
           ,[GuestId]
           ,[RoomId]
           ,[StaffId]
           ,[ServiceId]
           ,[DateCheckIn]
           ,[DateCheckout]
           ,[Price]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[DeletedAt])
     VALUES
           ('047bb9a5-fcf7-47b5-b2d2-e8432b144771'
           ,'b6281743-ce00-47c0-b2f9-6527c751e1ec'
           ,'a1d7a1e2-595b-4320-bda5-15b692d80a8d'
           ,'6c519692-e954-4563-ac3b-f17b3cabe993'
           ,'478bbc00-ba43-4469-a6fb-9df48272e4cb'
           ,'e38ceb94-ea7f-4f8b-a980-8ee8072e984b'
           ,GETDATE()
           ,GETDATE()
           ,40000
            ,GETDATE()
           ,'insert'
           ,GETDATE()
           ,'insert'
           ,null)
           ```
