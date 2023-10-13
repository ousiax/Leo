﻿using HoneyLovely.Models;

namespace HoneyLovely
{
    public interface IMemberService
    {
        Task<List<Member>> GetAsync();

        Task<int> CreateAsync(Member member);

        Task<int> UpdateAsync(Member member);
    }
}