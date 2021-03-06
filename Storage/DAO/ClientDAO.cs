﻿using System.Collections.Generic;
using System.Linq;
using Storage.Helpers;
using Storage.Models;
using Storage.ORM;

namespace Storage.DAO
{
    public static class ClientDAO
    {
        public static ClientModel GetClient(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            ClientModel client = (from c in storageDbEntities.Clients
                                    where c.ID == id && c.UserID == UserHelper.UserID
                                    select new ClientModel
                                               {
                                                   ID = c.ID,
                                                   Name = c.Name,
                                                   Address = c.Address,
                                                   Telephone = c.Telephone
                                               }).SingleOrDefault();
            return client;
        }

        public static List<ClientModel> GetClientList()
        {
            var storageDbEntities = new StorageDBEntities();

            List<ClientModel> list = (from c in storageDbEntities.Clients
                                      where c.UserID == UserHelper.UserID
                                      select new ClientModel
                                      {
                                          ID = c.ID,
                                          Name = c.Name,
                                          Address = c.Address,
                                          Telephone = c.Telephone
                                      }).ToList();

            return list;
        }

        public static void CreateClient(ClientModel clientModel)
        {
            if (UserHelper.UserID != null)
            {
                var storageDbEntities = new StorageDBEntities();

                Client newClient = new Client
                                       {
                                           Name = clientModel.Name,
                                           Address = clientModel.Address,
                                           Telephone = clientModel.Telephone,
                                           UserID = UserHelper.UserID.Value
                                       };

                storageDbEntities.Clients.AddObject(newClient);

                storageDbEntities.SaveChanges();
            }
        }

        public static void UpdateClient(int id, ClientModel clientModel)
        {
            var storageDbEntities = new StorageDBEntities();

            Client client = storageDbEntities.Clients.Where(c => c.ID == id && c.UserID == UserHelper.UserID).FirstOrDefault();

            if (client != null)
            {
                client.Name = clientModel.Name;
                client.Address = clientModel.Address;
                client.Telephone = clientModel.Telephone;
            }

            storageDbEntities.SaveChanges();
        }

        public static void DeleteClient(int id)
        {
            var storageDbEntities = new StorageDBEntities();

            Client client = storageDbEntities.Clients.Where(c => c.ID == id && c.UserID == UserHelper.UserID).FirstOrDefault();

            storageDbEntities.Clients.DeleteObject(client);

            storageDbEntities.SaveChanges();
        }
    }
}