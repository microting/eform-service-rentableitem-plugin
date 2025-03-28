/*
The MIT License (MIT)
Copyright (c) 2007 - 2025 Microting A/S
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microting.eForm.Dto;
using Microting.eForm.Infrastructure.Constants;
using Microting.eForm.Infrastructure.Models;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using Rebus.Handlers;
using ServiceRentableItemsPlugin.Infrastructure.Helpers;
using ServiceRentableItemsPlugin.Messages;


namespace ServiceRentableItemsPlugin.Handlers;

public class eFormRetrievedHandler : IHandleMessages<eFormRetrieved>
{
    private readonly eFormCore.Core _sdkCore;
    private readonly eFormRentableItemPnDbContext _dbContext;

    public eFormRetrievedHandler(eFormCore.Core sdkCore, DbContextHelper dbContextHelper)
    {
        _dbContext = dbContextHelper.GetDbContext();
        _sdkCore = sdkCore;
    }
    public async Task Handle(eFormRetrieved message)
    {
        ContractInspectionItem contractInspectionItem =
            _dbContext.ContractInspectionItem.SingleOrDefault(x => x.SDKCaseId == message.caseId);
        if (contractInspectionItem != null)
        {
            if (contractInspectionItem.Status < 77)
            {
                contractInspectionItem.Status = 77;
                await contractInspectionItem.Update(_dbContext);
            }
        }
    }
}