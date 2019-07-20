using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BSIndie.Models;
using BSIndie.Services;

using CodeHollow.FeedReader;

[assembly: Xamarin.Forms.Dependency(typeof(BSIndie.Services.DataStore))]
namespace BSIndie.Services
{
    public class DataStore : IDataStore<Item>
    {
        List<Item> items;

        public DataStore()
        {
            items = new List<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            Feed feed = await FeedReader.ReadAsync("https://www.bishopsstortfordindependent.co.uk/_api/rss/bishops_stortford_news_feed.xml");

            foreach (var item in feed.Items)
            {
                items.Add(new Item { Id = Guid.NewGuid().ToString(), Title = item.Title, Description = item.Description, PublishingDate = (DateTime)item.PublishingDate, Author = item.Author, Link = item.Link});
            }

            //removed as not utlised by indie
            /*
            //add keywords after loading items
            var keywords = Regexes.Keywords.Matches(feedSrc);
            for (int i = 0; i < keywords.Count; i++)
            {
                items[i].KeyWords = keywords[i].Value;
            }*/

            return await Task.FromResult(items);
        }
    }
}