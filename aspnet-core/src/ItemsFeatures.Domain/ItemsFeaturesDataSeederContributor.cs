using ItemsFeatures.Categories;
using ItemsFeatures.CategoriesItems;
using ItemsFeatures.Items;
using ItemsFeatures.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ItemsFeatures
{
    class ItemsFeaturesDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Item, Guid> _itemRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<CategoryItem, Guid> _categoryItemRepository;

        public ItemsFeaturesDataSeederContributor(IRepository<Item, Guid> itemRepository, IRepository<Unit, Guid> unitRepository, IRepository<Category, Guid> categoryRepository, IRepository<CategoryItem, Guid> categoryItemRepository)
        {
            _itemRepository = itemRepository;
            _unitRepository = unitRepository;
            _categoryRepository = categoryRepository;
            _categoryItemRepository = categoryItemRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _unitRepository.GetCountAsync() <= 0)
            {
                Unit FirstUnit =  await _unitRepository.InsertAsync(
                    new Unit
                    {
                        Name = "First Unit"
                    
                    },
                    autoSave: true
                );

                Unit SecondUnit = await _unitRepository.InsertAsync(
                  new Unit
                  {
                      Name = "Second Unit"

                  },
                  autoSave: true
              );

                if (await _itemRepository.GetCountAsync() <= 0)
                {
                    Item FirstItem = await _itemRepository.InsertAsync(
                        new Item
                        {
                            Name = "First Item",
                            UnitId = FirstUnit.Id,
                            
                        },
                        autoSave: true
                    );

                    Item SecondItem = await _itemRepository.InsertAsync(
                         new Item
                         {
                             Name = "Second Item",
                             UnitId = FirstUnit.Id,

                         },
                         autoSave: true
                     );

                    Item ThirdItem = await _itemRepository.InsertAsync(
                        new Item
                        {
                            Name = "Third Item",
                            UnitId = SecondUnit.Id,

                        },
                        autoSave: true
                    );

                    Item FourthItem = await _itemRepository.InsertAsync(
                         new Item
                         {
                             Name = "Fourth Item",
                             UnitId = SecondUnit.Id,

                         },
                         autoSave: true
                     );

                    if (await _categoryRepository.GetCountAsync() <= 0)
                    {
                        Category FirstCategory = await _categoryRepository.InsertAsync(
                            new Category
                            {
                                Name = "First Category",

                            },
                            autoSave: true
                        );

                        Category SecondCategory = await _categoryRepository.InsertAsync(
                             new Category
                             {
                                 Name = "Second Category",
                                 ParentId = FirstCategory.Id,

                             },
                             autoSave: true
                         );

                        Category ThirdCategory = await _categoryRepository.InsertAsync(
                          new Category
                          {
                              Name = "Third Category",
                              ParentId = FirstCategory.Id,

                          },
                          autoSave: true
                      );

                        if (await _categoryItemRepository.GetCountAsync() <= 0)
                        {
                            CategoryItem FirstCategoryItem = await _categoryItemRepository.InsertAsync(
                                new CategoryItem
                                {
                                    ItemId = FirstItem.Id,
                                    CategoryId = FirstCategory.Id

                                },
                                autoSave: true
                            );

                            CategoryItem SecondCategoryItem = await _categoryItemRepository.InsertAsync(
                                 new CategoryItem
                                 {
                                     ItemId = SecondItem.Id,
                                     CategoryId = SecondCategory.Id

                                 },
                                 autoSave: true
                             );

                            CategoryItem ThirdCategoryItem = await _categoryItemRepository.InsertAsync(
                                  new CategoryItem
                                  {
                                      ItemId = ThirdItem.Id,
                                      CategoryId = ThirdCategory.Id

                                  },
                                  autoSave: true
                              );

                            CategoryItem FourthCategoryItem = await _categoryItemRepository.InsertAsync(
                                 new CategoryItem
                                 {
                                     ItemId = FourthItem.Id,
                                     CategoryId = SecondCategory.Id

                                 },
                                 autoSave: true
                             );
                         await _categoryItemRepository.InsertAsync(
                             new CategoryItem
                             {
                                 ItemId = FourthItem.Id,
                                 CategoryId = ThirdCategory.Id

                             },
                             autoSave: true
                         );



                        }
                    }

                   
                }

    
            }
            
        }
    }
}