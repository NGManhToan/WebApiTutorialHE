﻿using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IPostQuery
    {
        Task<List<HomePostModel>> QueryHomePost();
        Task<List<HomePostModel>> QueryFindPost(string search);

        //Lấy ds các vật phẩm theo categoryId
        Task<List<HomePostModel>> QuerySelectPostFollowCategoryId(int id);

    }
}
