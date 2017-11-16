using System;
using System.Collections.Generic;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace referenceguide
{
    public class DataBusinessLayer : CoreBusiness
    {
        public IBackgroundTimer TimerService
        {
            get { return this.BackgroundTimer; }
        }

        public async Task<(T Response, Exception Error)> GetFileData<T>(string resourceName) where T : class, new()
        {
            try
            {
                var data = await FileStore.GetAsync<T>(resourceName);
                data.Error?.LogException("DataBusinessLayer - GetFileData");
                return (data.Response, data.Error);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - GetFileData");
                return (null, ex);
            }
        }

        public async Task<(bool Success, Exception Error)> SaveFileData<T>(string resourceName, T obj) where T : class, new()
        {
            try
            {
                var data = await FileStore.SaveAsync<T>(resourceName, obj);
                data.Error?.LogException("DataBusinessLayer - SaveFileData");
                return (data.Success, data.Error);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - SaveFileData");
                return (false, ex);
            }

        }


        public List<FontItemRow> GetFontList(Dictionary<string, char> dict, string fontFamily)
        {
            var temp = new List<FontItemRow>();
            var col = 0;
            var row = 0;

            foreach (var key in dict.Keys)
            {
                if (col == 3)
                {
                    col = 0;
                    row++;
                }

                FontItemRow rowItem = null;
                if (col == 0)
                {
                    rowItem = new FontItemRow() { Row = row };
                    temp.Add(rowItem);
                }
                else
                {
                    rowItem = temp.First(x => x.Row == row);
                }

                var item = new FontItem()
                {
                    FriendlyName = key,
                    FontFamily = fontFamily,
                    Unicode = dict[key].ToString()
                };

                switch (col)
                {
                    case 0:
                        rowItem.Item1 = item;
                        break;
                    case 1:
                        rowItem.Item2 = item;
                        break;
                    case 2:
                        rowItem.Item3 = item;
                        break;
                }

                col++;
            }

            return temp;
        }

        public void CreateFictiousError()
        {
            try
            {
                var x = 10;
                var y = 0;
                var total = x / y;
                Console.WriteLine($"The total is {total}");
            }
            catch (Exception ex)
            {
                ex.ConsoleWrite(true);
                ex?.LogException("DataBusinessLayer - CreateFictiousError");
            }
        }

        public async Task<(bool Success, Exception Error)> ClearLogFiles(LogType type)
        {
            try
            {
                await Log.ClearLogging(type);
                return (true, null);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - ClearLogFiles");
                return (false, ex);
            }
        }
        public async Task<(List<T> Response, Exception Error)> GetLogFiles<T>() where T : new()
        {
            try
            {
                var n = typeof(T).FullName;
                IList aData = null;
                var aList = new List<T>();
                (List<T> Response, Exception Error) result = (null, null);

                switch (n)
                {
                    case "Xamarin.Forms.CommonCore.AnalyticLog":
                        aData = await Log.GetHistoricalLogs(LogType.Analytic);
                        break;
                    case "Xamarin.Forms.CommonCore.ErrorLog":
                        aData = await Log.GetHistoricalLogs(LogType.Error);
                        break;
                    default:
                        result.Error = new ApplicationException("Data type is not a log type");
                        break;
                }

                foreach (var obj in aData)
                    aList.Add((T)obj);
                result.Response = aList;
                return result;
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - GetLogFiles");
                return (null, ex);
            }
        }
        public async Task<(bool Response, Exception Error)> SaveAppointment(Appointment appt)
        {
            var result = await SqliteDb.AddOrUpdate<Appointment>(appt);
            result.Error?.LogException("DataBusinessLayer - SaveAppointment");
            return (result.Success, result.Error);
        }
        public async Task<(List<Appointment> Response, Exception Error)> GetAllAppointments()
        {
            try
            {
                var data = await this.SqliteDb.GetByQuery<Appointment>((x) => x.MarkedForDelete == false);
                data.Error?.LogException("DataBusinessLayer - GetAllAppointments");
                return (data.Response, data.Error);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - GetAllAppointments");
                return (null, ex);
            }
        }

        public (List<CarouselBindingObject> Response, Exception Error) GetCarouselData()
        {
            try
            {
                var list = new List<CarouselBindingObject>();
                list.Add(new CarouselBindingObject() { Position = 1, ImageUrl = "snowfall.jpeg" });
                list.Add(new CarouselBindingObject() { Position = 2, ImageUrl = "rain.jpeg" });
                list.Add(new CarouselBindingObject() { Position = 3, ImageUrl = "beach.jpeg" });
                list.Add(new CarouselBindingObject() { Position = 4, ImageUrl = "sunset.jpeg" });
                return (list, null);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - GetCarouselData");
                return (null, ex);
            }
        }

        public (List<Person> Response, Exception Error) GetPeople(string last)
        {
            try
            {
                var list = new List<Person>();
                list.Add(new Person() { FName = "Jack", LName = "Sparrow" });
                list.Add(new Person() { FName = "Charles", LName = "Brown" });
                list.Add(new Person() { FName = "Mack", LName = "Smith" });
                list.Add(new Person() { FName = "Henry", LName = "Abrams" });
                list.Add(new Person() { FName = "Milo", LName = "Davis" });
                list.Add(new Person() { FName = "Will", LName = "Piles" });
                list.Add(new Person() { FName = "Quincy", LName = "Yales" });
                list.Add(new Person() { FName = "Sally", LName = "Yules" });
                list.Add(new Person() { FName = "Thad", LName = "Dallon" });
                list.Add(new Person() { FName = "Randy", LName = "Tarvis" });
                list.Add(new Person() { FName = "Barry", LName = "Red" });
                list.Add(new Person() { FName = "Valerie", LName = "Spencer" });
                list.Add(new Person() { FName = "Mike", LName = "Spiegal" });
                list.Add(new Person() { FName = "Bob", LName = "Adams" });
                list.Add(new Person() { FName = "Ralph", LName = "Braun" });
                list.Add(new Person() { FName = "Vicky", LName = "Punders" });
                list.Add(new Person() { FName = "Micky", LName = "York" });
                list.Add(new Person() { FName = "Sam", LName = "Williams" });
                list.Add(new Person() { FName = "Hal", LName = "Weeks" });
                list.Add(new Person() { FName = "Paul", LName = "Gillis" });
                list.Add(new Person() { FName = "Nathan", LName = "Spears" });
                list.Add(new Person() { FName = "Eagar", LName = "Poe" });
                list.Add(new Person() { FName = "Greg", LName = "Gale" });
                list.Add(new Person() { FName = "Jules", LName = "Verner" });


                if (!string.IsNullOrEmpty(last))
                {
                    list = list.Where(x => x.LName.StartsWith(last, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                return (list, null);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - GetPeople");
                return (null, ex);
            }

        }

        public (List<State> Response, Exception Error) GetAllStates()
        {
            try
            {
                var lst = new List<State>(new State[] {
                    new State { Value = "AL", Text = "Alabama" },
                    new State { Value = "AK", Text = "Alaska" },
                    new State { Value = "AZ", Text = "Arizona" },
                    new State { Value = "AR", Text = "Arkansas" },
                    new State { Value = "CA", Text = "California" },
                    new State { Value = "CO", Text = "Colorado" },
                    new State { Value = "CT", Text = "Connecticut" },
                    new State { Value = "DE", Text = "Delaware" },
                    new State { Value = "FL", Text = "Florida" },
                    new State { Value = "GA", Text = "Georgia" },
                    new State { Value = "HI", Text = "Hawaii" },
                    new State { Value = "ID", Text = "Idaho" },
                    new State { Value = "IL", Text = "Illinois" },
                    new State { Value = "IN", Text = "Indiana" },
                    new State { Value = "IA", Text = "Iowa" },
                    new State { Value = "KS", Text = "Kansas" },
                    new State { Value = "KY", Text = "Kentucky" },
                    new State { Value = "LA", Text = "Louisiana" },
                    new State { Value = "ME", Text = "Maine" },
                    new State { Value = "MD", Text = "Maryland" },
                    new State { Value = "MA", Text = "Massachusetts" },
                    new State { Value = "MI", Text = "Michigan" },
                    new State { Value = "MN", Text = "Minnesota" },
                    new State { Value = "MS", Text = "Mississippi" },
                    new State { Value = "MO", Text = "Missouri" },
                    new State { Value = "MT", Text = "Montana" },
                    new State { Value = "NC", Text = "North Carolina" },
                    new State { Value = "ND", Text = "North Dakota" },
                    new State { Value = "NE", Text = "Nebraska" },
                    new State { Value = "NV", Text = "Nevada" },
                    new State { Value = "NH", Text = "New Hampshire" },
                    new State { Value = "NJ", Text = "New Jersey" },
                    new State { Value = "NM", Text = "New Mexico" },
                    new State { Value = "NY", Text = "New York" },
                    new State { Value = "OH", Text = "Ohio" },
                    new State { Value = "OK", Text = "Oklahoma" },
                    new State { Value = "OR", Text = "Oregon" },
                    new State { Value = "PA", Text = "Pennsylvania" },
                    new State { Value = "RI", Text = "Rhode Island" },
                    new State { Value = "SC", Text = "South Carolina" },
                    new State { Value = "SD", Text = "South Dakota" },
                    new State { Value = "TN", Text = "Tennessee" },
                    new State { Value = "TX", Text = "Texas" },
                    new State { Value = "UT", Text = "Utah" },
                    new State { Value = "VT", Text = "Vermont" },
                    new State { Value = "VA", Text = "Virginia" },
                    new State { Value = "WA", Text = "Washington" },
                    new State { Value = "WV", Text = "West Virginia" },
                    new State { Value = "WI", Text = "Wisconsin" },
                    new State { Value = "WY", Text = "Wyoming" }
                });
                return (lst, null);
            }
            catch (Exception ex)
            {
                ex?.LogException("DataBusinessLayer - GetAllStates");
                return (null, ex);
            }
        }
    }
}
