using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public enum Gender
    {
        Male,Female,Other
    }

    public enum BlogCategories
    {
        Art, Automotive, Beauty , Business,Comedy,Education,Entertainment,
        EyeCandy , Family , Fashion , Food, Gaming , Good , Health , HigherPower,
        HowTo,Journals,Lifestyle , Men , MoviesAndTv , Music , News , Opinionated,
        Outstanding , PetsAndAnimals , Photography , Politics , Relationships ,
        Science , Self, Society , Sports , Tech , Travel , Women , Writing
    }

    public enum FileType
    {
        BlogImage , UserImage , HtmlText
    }

    public enum NotificationType 
    { 
        UserFollowed,
        BlogSubscribed,
        PostCommentedUpon,
        PostLiked
    }

    public enum ActivityType 
    {
        UserFollowUser,
        UserSubscribesBlog,
        UserCommentPost,
        BlogGeneratePost,
        UserCreateBlog,UserLikesPost
    }
} 
