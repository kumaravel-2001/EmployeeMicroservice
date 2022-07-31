using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Domain.Entities
{
    public class EntityBase   //to create entity with unique Id
    {
        public int Id { get; set; }
        public override bool Equals(object obj)
        {
            EntityBase entityBase = obj as EntityBase; //create object as entitybase
            if (ReferenceEquals(this, entityBase))  //checks 2 entities are same based on reference
                return true;
            else if (ReferenceEquals(this, entityBase))
                return false;
            else if (this.GetType().Name != entityBase.GetType().Name) //checks 2 entities are same based on type
                return false;
            return this.Id == entityBase.Id; //checks 2 entities are same based on Id

        }
        public override int GetHashCode()
        {
            return (this.GetType().FullName + Id).GetHashCode(); //generating hashcode based on fully qualified name and id 
        }

    }
}
