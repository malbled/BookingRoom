
# Тема - автоматизация бронирования в отеле

Выполнила - Малышева Александра Юрьевна гр.ИП 20-3
ㅤ
ㅤ
ㅤ

#### Пример бизнес сценария - информационный чек


![image](https://github.com/malbled/BookingRoom/assets/107112651/668a6d3e-f315-4a9f-827b-75f5248ce63e)

## Схема моделей

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
