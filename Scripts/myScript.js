  
  var months=['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September','October', 'November', 'December'];
  // months which have 31 days;
  var days_ThirtyOne=[1, 3, 5, 7, 8, 10, 12];
    // months which have 30 days;
  var days_Thirty=[4, 6, 9, 11];

//On page load, this function is called.
  $(document).ready(function(){

              //adding months to select
              var listHtml="<option selected disabled>Choose a month</option>";
              $.each(months,function(i) {
              listHtml+="<option>"+months[i]+"</option>";
              });
              $('.monthsul').html(listHtml);


              // on submitting form this function is called   
              $('.formB').click(function(){
                //capturing month,name and date from form
              var selectedMonth=$('.monthsul :selected').index();
              var nameInput=$('#nameInput').val();
              var dayInput=$('#dayInput').val();
              //capturing captcha values which are to be validated
              var recaptcha_challenge_fieldd=$("input[name='recaptcha_challenge_field']" ).val();
              var recaptcha_response_fieldd=$("input[name='recaptcha_response_field']" ).val();
               if(selectedMonth==0)
               {
                $('#errormsg').text("Please select month");
                return false;
              }
              // day must be a number, should not validate any text
              else if(isNaN(dayInput))
              {
                 $('#errormsg').text("Day is supposed to be a number.");
                 return false;
              }
              var flag=verifyDayforMonth(dayInput,selectedMonth); // functionCall
              if(flag==false || flag==undefined)
              {
                 $('#errormsg').text("Invalid day for the selected month : "+$('.monthsul :selected').val()); 
                return false;
              }
              //After checking all the errors, Now send data to process-submission.php to validate captcha
              $.ajax({
                    type: "POST",
                    url: "process-submission.php",
                    cache: false,
                    data: {name: nameInput, birth_month: selectedMonth, birth_day:dayInput,recaptcha_challenge_field:recaptcha_challenge_fieldd, recaptcha_response_field:recaptcha_response_fieldd},
                    success: submissionResultHandler,
                    error: onError,
                    dataType: 'json'
                });
            });
  });

//on success of POST request this function is called
function submissionResultHandler(result){
           // if captcha value is wrong, display error message.
            if(result.validCaptcha=="no"){
              alert("Invalid text in captcha ");
              location.reload();
            }
            else
              //display the result with message
            {
              $('.form').hide();
              $('.resultdiv').show();
              var resultdiv="<br>&nbsp&nbsp<h3>Hello "+result.username+", <p>Your sign is "+(result.name)+", "+result.nickName +".</p></h3>&nbsp<img src="+result.image+">";
              $('.resultdiv').html(resultdiv);
            }
}

function onError(){
        alert("Error in form submission");
}

/* 
This funciton verifies the day month combination; for example if selected month is january, it has to consider day from 1 to 31 else return false. similarly for feb: 1 to 29
*/
function verifyDayforMonth (day, selectedMonth) {
                if(jQuery.inArray(selectedMonth,days_ThirtyOne)>=0)
                {
                  if(day<1 || day>31)
                  {
                   
                    return false;
                  }
                }
                if(jQuery.inArray(selectedMonth,days_Thirty)>=0)
                {
                  if(day<1 || day>30)
                  {
                    return false;
                  }
                }
                if(selectedMonth==2)
                {
                  if(day<1 || day>29)
                  {
                    return false;
                  }
                }
                return true;
}


