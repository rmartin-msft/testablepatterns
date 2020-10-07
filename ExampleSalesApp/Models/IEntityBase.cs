//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Models
{
    public interface IEntityBase
    {
        int Id { get; set; }
    }
}
