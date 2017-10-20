namespace Sales.Domain.Aggregates
{
    public class Person
    {
        public Person(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        public string FirstName { get;  }
        public string LastName { get;   }
        public string Phone { get;      }
        public string Email { get;      }
    }
}