$(document).ready(function () {
		    //$(document).bind('pageinit');
		    $("#next").hide();
		    $("#finish").hide();
		    $("#pump").hide();
		    $("#done").hide();
		    //$("#start").show();

		
		    $("#done").click(function(){
		        $("#next").show();
		        $("#pump").show();
		        $("#done").hide();
		        $("#start").hide();
		    });
		  
		    $("#next").click(function(){
		        $("#next").hide();
		        $("#pump").hide();
		        $("#done").show();
		        $("#start").hide();
		        next();
		    });
		  
		    $("#start").click(function(){
		        $("#next").hide();
		        $("#pump").hide();
		        $("#done").show();
		        $("#start").hide();
		        next();
		    }); 
		
		    $("#finish").click(function(){
		        cleartask();
		        window.location.href = "#home";
		    }); 

		  
		    $("#reset").click(function(){
		        location.reload();
		    });
		
		
		    //Clear seesion data when going to another task
		    $("#clearrun").click(function(){
		        cleartask();
		    }); 
		
		    $("#gettaskapi").click(function(){
		        getTasks();
		    });
		
		    var wait = 0;
		
		    //When a user clicks on a task get the id of the task to pass to api to pull the correct task
		            var tasknum = '17';
		            //prints to console for debugging
		            //console.log(task);
		            //fucntion that loads the tasks
		            sessionStorage.setItem("task", tasknum);
		            sessionStorage.setItem("stepnum", 0);
		            //run();
		            window.location.href = "#dotask";
		            getMainSteps();
		
		    function cleartask() {
		        var keepuid = sessionStorage.getItem("UserId");
		        sessionStorage.clear();
		        sessionStorage.setItem("UserId", keepuid);
		        sessionStorage.setItem("stepnum", 0);
		    }
			
		    function getTasks() {
		        //Change page to task page
		        window.location.href = "#task";
		        //Username
		        var id = sessionStorage.getItem("UserId");
		        //alert(id);
		        //password
		        var password = $("#password").val();
		        //sets api location to include user id
		        var api = ('http://ipawsteamb.csweb.kutztown.edu/api/task/GetTaskByCategoryId/1');
		        //alert(api);
		        $('#listtask').empty();
		        $('#listtask').append('<li data-role="list-divider"><h2>Choose a task from below or Search for a task by typing the task name into the search box above!</h2></li>');
				
		        //Team A user API
		        $.getJSON(api,
                    function (data) {
                        //var rows = "";
                        // Loop through the list of users.
                        $.each(data, function (key, val) {
                            sessionStorage.setItem(val.taskId, val.title);
                            $("#listtask").append('<li class="taskpage" id="'+ val.taskId +'"><a>' + val.title + '</a></li>');
                            $('#listtask').listview().listview('refresh');
                            //Add a table row for each product.
                            //rows += '<li>' + val.title + '</li>';          
                        });
                        //document.getElementById('listtask').innerHTML = rows;
                    });
		    } //end gettask
			
		    function getMainSteps() {
		        alert('inside main step');
		        //var task = $(this).attr('name');
		        var task = sessionStorage.getItem("task");
		        //alert(task);
		        //window.location.href = "#dotask";
		        var api = ("http://ipawsteamb.csweb.kutztown.edu/api/MainStep/GetMainStepByTaskId/1");
		        var total = 0;
		        $("#bot").empty();
		        //var step = '{"tasks":[]}';
		        //Team A user API
		        $.getJSON(api,
                    function (data) {
                        // Loop through the steps.
                        $.each(data, function (key, val) {
                            total += 1;
                            var taskdet = new Task(val.mainStepId, val.mainStepName, val.mainStepText, val.audioPath, val.videoPath);
                            var store = JSON.stringify(taskdet);
                            sessionStorage.setItem(total, store);
                            //alert(JSON.stringify(JSON.parse(sessionStorage.getItem(total))));
                            //alert("debug" + taskdet.text);
                            //getDetSteps(total);
                            sessionStorage.setItem('maintotal', total);
                            //
                            $("#bot").append('<li id="step' + total + '">' + val.mainStepName + '</li>');
                            $('#bot').listview().listview('refresh');
                            //$("#steptitle").text(sessionStorage.getItem("1"));      
                        });
                        //document.getElementById('listtask').innerHTML = rows;
                    });
		        //obj = JSON.parse(step);
		        //sessionStorage.setItem(steps, JSON.parse(text));
		        //$("#steptitle").text(sessionStorage.getItem(task));
		        //load detailed steps
		        $("#start").show();
		    } //end get main steps
			
		    function Task(stepId, text, audio, video, image){
		        this.stepId = stepId;
		        this.text = text;
		        this.audio = audio;
		        this.video = video;
		        this.image = image;
        
		    }
		    function getDetSteps(id) {
		        //var task = $(this).attr('name');
		        //var detnum = sessionStorage.getItem("step" + total);
		        //alert(detnum);
		        //window.location.href = "#dotask";
		        var api = ("http://ipawsteamb.csweb.kutztown.edu/api/DetailedStep/GetDetailedStepById/" + id);
		        var dtotal = 0;
		        //$("#detailstep").empty();
		        //Team A user API
		        $.getJSON(api,
                    function (data) {
                        dtotal += 1;
                        console.log(dtotal);
                        // Loop through the steps.
                        $.each(data, function (key, val) {
                            console.log("Detailed Steps " + val);
                            //dtotal += 1;
                            //sessionStorage.setItem(total + "dstep" + dtotal, val.text);
                            //sessionStorage.setItem(total + "dtotal", dtotal);
                            $("#detailstep").append('<p id="step' + val.detailedStepId + '">Step: ' + dtotal + ': ' + val.detailedStepText + '</p>');
                            if (val.image != null) {
                                $("#image").append('<p><img src="http://' + val.imagePath + '" height="200" alt="' + val.imageName + '" /></p>');
                            }
                            //$('#bot').listview().listview('refresh');;
                            //$("#steptitle").text(sessionStorage.getItem("1"));      
                        });
                        //document.getElementById('listtask').innerHTML = rows;
                    });
		        //$("#steptitle").text(sessionStorage.getItem(task));
					
		    } //end get det steps
			
		    function sleep(milliseconds) {
		        var start = new Date().getTime();
		        for (var i = 0; i < 1e7; i++) {
		            if ((new Date().getTime() - start) > milliseconds){
		                break;
		            }
		        }
		    }
		    /*function run() {
				var task = sessionStorage.getItem("task");
				
					getMainSteps(task);
					//getDetSteps(det);
				sessionStorage.setItem("stepnum", 1);
				while (JSON.stringify(JSON.parse(sessionStorage.getItem(1))) === null){
				console.log(JSON.stringify(JSON.parse(sessionStorage.getItem(1))));
				}
				sleep(5000);
				setpage();
				}//end get det steps*/
		
		    function next() {
			
		        var stepup = sessionStorage.getItem("stepnum");
		        stepup = parseInt(stepup);
		        $('#step' + stepup).prepend('completed - ');
		        stepup +=1;
		        console.log("stepup" + stepup);
		        sessionStorage.setItem("stepnum", stepup);
		        var end = sessionStorage.getItem("maintotal");
		        if (end == stepup) {
		            setpage();
		            $("#next").hide();
		            $("#done").hide();
		            $("#finish").show();
		        }
		        else {
		            setpage();
		            //alert("Setpage run");
		        }
				
		    }
			
		
		    function setpage() {
		        //alert("this is set page");
		        var step = sessionStorage.getItem("stepnum");
		        stepup = parseInt(step);
		        console.log(step);
		        //obj = JSON.parse(text);

		        //obj.employees[2].firstName + " " + obj.employees[2].lastName;
		        console.log(JSON.parse(sessionStorage.getItem(step)));
		        var test = JSON.parse(sessionStorage.getItem(step));
		        //alert(test);
		        //var temp = test;
		        //obj = JSON.parse(test);
		        //alert("parse" + test.text);
		        //var title = ('step' + step);
		        $("#steptitle").text(test.text); 
		        $("#av").empty();
		        if (test.video != null) {
		            $("#av").append('<video width="400" controls><source src="http://' + test.video + '" type="../../video/mp4"><img src="images/video.png" border="0" height="50px" /></video>');
		        }
		        if (test.audio != null) {
		            $("#av").append('<audio width="400" controls><source src="http://' + test.audio + '" type="../../audio/mp3"><img src="images/audio.png" border="0" height="50px" /></audio>');
		        }
		        $("#detailstep").empty();
		        $("#image").empty();
		        var id = (test.stepId);
		        console.log("id" + id);
		        getDetSteps(id);
		        //var max = sessionStorage.getItem(step + 'dtotal');
		        //JSON.parse(max);
		        //for (var i=1; i  <=  max; i++){
		        //var key = sessionStorage.getItem(step + 'dstep' + i);
		        //var value = sessionStorage.getItem(step + 'dstep' + i);
		        //alert(key + "=" + value);
		        //$("#detailstep").append('<p>Step: '+ i + ': ' + value + '</p>');
		        //}
		    }
		}); //end doc ready function