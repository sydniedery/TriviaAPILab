# TriviaAPILab

# Problem: Couldn't figure out how to get my key added because it is an extra header
# Solution: Had to rearrange the order of my header statements. 

# Problem: .ReadAsAsync was not showing as a known method
# Solution: Installed NuGet Package Microsoft.AspNet.WebApi.Client

# Problem: Deserialization method was not showing as known method. 
# Solution: Installed NuGet package Newtonsoft.Json

# Problem: Would not get data from API and put it into object
# Solution: Placed breakpoint and stepped through the code. Try catch block caught the exception "Cannot deserialize the current JSON array (e.g. [1,2,3]) into type 'ApiLab.Result' because the type requires a JSON object (e.g. {"name":"value"}) to deserialize correctly.
# To fix this error either change the JSON to a JSON object (e.g. {"name":"value"}) or change the deserialized type to an array or a type that implements a collection interface (e.g. ICollection, IList) like List<T> that can be deserialized from a JSON array. JsonArrayAttribute can also be added to the type to force it to deserialize from a JSON array.
# Path '', line 1, position 1." After some googling, I realized this meant I did not have my classes set up correctly to store the data. I fought with it for about an hour and couldn't get it right so I scratched that API and chose a different one that returned the JSON data in a much better way. I still had this error for a minute, I realized I could not use ".ReadAsAsync" method with just a single QuestionClass object. I had to use  .ReadAsAsync<IEnumerable<Questions>>() even though I am only getting one question at a time. 
