using System.Linq;
using System.Collections.Generic;
using Xunit;
using WebApi.Controllers;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Moq;
public class BlogControllerTests
{
    private readonly BlogController _blogController;
    private Mock<List<Post>> _mockPostsList;
    public BlogControllerTests()
    {
        _mockPostsList = new Mock<List<Post>>();
        _blogController = new BlogController(_mockPostsList.Object);
    }

    private Post GenerateAMockPost()
    {
        return new Post(){Id= Guid.NewGuid(),Title="test", Content="test content", Author = "Author1", Image = "", Category = "Technology", Tags =new List<string>(){"Programming","C#","Cloud"},
        CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now};    
    }

    private List<Post> GenerateMockPosts()
    {
        return Enumerable.Range(0, 10).Select(x => GenerateAMockPost()).ToList<Post>();

    }


    [Fact]
    public void GetPosts_ReturnsListofPosts()
    {
        //Arrange
        var Posts = GenerateMockPosts();
        _mockPostsList.Object.AddRange(Posts);
        
        //Act
        var result = _blogController.GetPosts();

        //Assert
        var model = Assert.IsAssignableFrom<ActionResult<List<Post>>>(result);

    }

    #region BlogController.GetPostByID() Tests

        [Fact]
        public void GetPostById_WithUnexistingPost_ReturnsNotFound()
        {
            //arrange
            var post = new Post() { Id = new Guid() };

            _mockPostsList.Object.SingleOrDefault(m => m.Id == post.Id);

            //act
            var result = _blogController.GetPostById(post.Id);

            //assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetPostById_WithExistingPost_ReturnsThePost()
        {
            //arrange 
            var post = new Post() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f") ,
                Title ="Cloud Academy", Content ="The best platform for skill-based match making"};

            _mockPostsList.Object.Add(post);

            //act 
            var result = _blogController.GetPostById(post.Id);

            //assert 
            var model = Assert.IsType<ActionResult<Post>>(result);
            Assert.Equal(post, model.Value);
        }

    #endregion

    #region BlogController.AddPost() Tests
    
    [Fact]
    public void AddPost_WithValidData_ReturnsOk()
    {
        //Arrange
       
        var newPost = GenerateAMockPost();
        //Act
        var result = _blogController.AddPost(newPost);

        //Assert
        Assert.IsAssignableFrom<CreatedAtActionResult>(result);
    }

    [Fact]
    public void AddPost_WithContentOver1024Chars_Returns400Error()
    {
        //Arrange
        var newPost = GenerateAMockPost();
        newPost.Content = new string('*',1025);
        //Act
        var result = _blogController.AddPost(newPost);
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);
    }

    [Fact]
    public void AddPost_WithEmptyTitle_Returns400Error()
    {
        //Arrange
        var newPost = GenerateAMockPost();
        newPost.Title = "";
        //Act
        var result = _blogController.AddPost(newPost);
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);
    }

    [Fact]
    public void AddPost_WithEmptyContent_Returns400Error()
    {
        //Arrange
        var newPost = GenerateAMockPost();
        newPost.Content = "";
        //Act
        var result = _blogController.AddPost(newPost);
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);
    }

    [Fact]
    public void AddPost_WithEmptyAuthor_Returns400Error()
    {
        //Arrange
        var newPost = GenerateAMockPost();
        newPost.Author = "";
        //Act
        var result = _blogController.AddPost(newPost);
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);
    }

    [Fact]
    public void AddPost_WithEmptyCategory_Returns400Error()
    {
        //Arrange
        var newPost = GenerateAMockPost();
        newPost.Category = "";
        //Act
        var result = _blogController.AddPost(newPost);
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);
    }
    #endregion

    #region BlogController.UpdatePost() Tests
    [Fact]
    public void UpdatePost_WithValidData_ReturnsOk()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var existingPost = _blogController.GetPosts().Value.FirstOrDefault();

        //Act
        var result = _blogController.UpdatePost(existingPost);
        
        //Assert
        Assert.IsAssignableFrom<OkResult>(result);

    } 
    
    [Fact]
    public void UpdatePost_WithEmptyTitle_Returns400Error()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var existingPost = _blogController.GetPosts().Value.FirstOrDefault();
        existingPost.Title = "";

        //Act
        var result = _blogController.UpdatePost( existingPost);
        
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);

    } 

    [Fact]
    public void UpdatePost_WithEmptyContent_Returns400Error()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var existingPost = _blogController.GetPosts().Value.FirstOrDefault();
        existingPost.Content = "";

        //Act
        var result = _blogController.UpdatePost( existingPost);
        
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);

    } 

    [Fact]
    public void UpdatePost_WithContentOver1024Chars_Returns400Error()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var existingPost = _blogController.GetPosts().Value.FirstOrDefault();
        existingPost.Content = new string('*',1025);

        //Act
        var result = _blogController.UpdatePost( existingPost);
        
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);

    } 

    

    [Fact]
    public void UpdatePost_WithEmptyAuthor_Returns400Error()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var existingPost = _blogController.GetPosts().Value.FirstOrDefault();
        existingPost.Author = "";

        //Act
        var result = _blogController.UpdatePost( existingPost);
        
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);

    } 

    [Fact]
    public void UpdatePost_WithEmptyCategory_Returns400Error()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var existingPost = _blogController.GetPosts().Value.FirstOrDefault();
        existingPost.Category = "";

        //Act
        var result = _blogController.UpdatePost( existingPost);
        
        //Assert
        Assert.IsAssignableFrom<BadRequestResult>(result);

    } 

    #endregion

    #region BlogController.DeletePost() Tests
    [Fact]
    public void DeletePost_WithExistingPost_ReturnsOk()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var post = _blogController.GetPosts().Value.FirstOrDefault();

        //Act
        var result = _blogController.DeletePost(post.Id.ToString());

        //Assert
        Assert.IsAssignableFrom<OkResult>(result);
    }

    [Fact]
    public void DeletePost_WithNonExistingPost_ReturnsNotFound()
    {
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var post = GenerateAMockPost();

        //Act
        var result = _blogController.DeletePost(post.Id.ToString());

        //Assert
        Assert.IsAssignableFrom<NotFoundResult>(result);
    }

    [Fact]
    public void DeletePost_WithNonAdminAccount_ReturnsUnauthorized()
    {//todo: continue writing tests for the rest of regions
        //Arrange
        _mockPostsList.Object.AddRange(GenerateMockPosts());
        var post = _mockPostsList.Object.FirstOrDefault();

        //Act

        var result = _blogController.DeletePost(post.Id.ToString());

        //Assert
        Assert.IsAssignableFrom<UnauthorizedResult>(result);
    }

    #endregion

    #region BlogController.SearchPost() Tests

    #endregion

    #region BlogController.AssignPostCategory() Tests
    #endregion

    #region BlogController.AssignPostTags() Tests
    #endregion
}