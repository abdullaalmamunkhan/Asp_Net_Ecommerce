using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProperties.Enums
{
    public enum ApplicationModules
    {
        Login,
        Category,
        Tags,
        ProductAttributes,
        AttributeItems,
        SliderImage,
        Products,
        User
    }

    public enum ApplicationActivity
    {
        Login = 1,
        Logout,
        Create,
        Update,
        Delete,
        Verify
    }
}
