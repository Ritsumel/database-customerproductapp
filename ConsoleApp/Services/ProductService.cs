﻿using ConsoleApp.Entities;
using ConsoleApp.Repositories;

namespace ConsoleApp.Services;

internal class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly CategoryService _categoryService;

    public ProductService(ProductRepository productRepository, CategoryService categoryService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
    }




    //CREATE
    public ProductEntity CreateProduct(string title, decimal price, string categoryName)
    {
        var categoryEntity = _categoryService.CreateCategory(categoryName);
        var productEntity = new ProductEntity
        {
            Title = title,
            Price = price,
            CategoryId = categoryEntity.Id
        };

        productEntity = _productRepository.Create(productEntity);
        return productEntity;
    }

    //READ
    public ProductEntity GetProductById(int id)
    {
        var productEntity = _productRepository.Get(x => x.Id == id);
        return productEntity;
    }

    public IEnumerable<ProductEntity> GetProducts()
    {
        var products = _productRepository.GetAll();
        return products;
    }

    //UPDATE
    public ProductEntity UpdateProduct(ProductEntity productEntity)
    {
        var updatedProductEntity = _productRepository.Update(x => x.Id == productEntity.Id, productEntity);
        return updatedProductEntity;
    }

    //DELETE
    public void DeleteProduct(int id)
    {
        _productRepository.Delete(x => x.Id == id);
    }



}
