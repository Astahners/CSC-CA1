var clarifaiApiKey = 'cef4b3e4d5794a3cb7a99105bb367707';
var workflowId = 'tests';

var app = new Clarifai.App({
    apiKey: clarifaiApiKey
});

// Handles image upload
function uploadImage() {
    var preview = document.querySelector('img');
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        preview.src = reader.result;
        var imageData = reader.result;
        imageData = imageData.replace(/^data:image\/(.*);base64,/, '');
        predictFromWorkflow(imageData);
    }, false);

    if (file) {
        reader.readAsDataURL(file);
        preview.style.display = "inherit";
    }
}

// Analyzes image provided with Clarifai's Workflow API
function predictFromWorkflow(photoUrl) {
    app.workflow.predict(workflowId, { base64: photoUrl }).then(
        function (response) {
            var outputs = response.results[0].outputs;
            var analysis = $(".analysis");

            analysis.empty();
            console.log(outputs);

            outputs.forEach(function (output) {
                var modelName = getModelName(output);

                // Create heading for each section
                var newModelSection = document.createElement("div");
                newModelSection.className = modelName + " modal-container";

                var newModelHeader = document.createElement("h2");
                newModelHeader.innerHTML = modelName;
                newModelHeader.className = "model-header";

                var formattedString = getFormattedString(output);
                var newModelText = document.createElement("p");
                newModelText.innerHTML = formattedString;
                newModelText.className = "model-text";

                newModelSection.append(newModelHeader);
                newModelSection.append(newModelText);
                analysis.append(newModelSection);
            });
        },
        function (err) {
            console.log(err);
        }
    );
}

// Helper function to get model name
function getModelName(output) {
    if (output.model.display_name !== undefined) {
        return output.model.display_name;
    } else if (output.model.name !== undefined) {
        return output.model.name;
    } else {
        return "";
    }
}

// Helper function to get output customized for each model
function getFormattedString(output) {
    var formattedString = "";
    var data = output.data;
    var maxItems = 3;
    // General
    if (output.model.model_version.id === "aaa03c23b3724a16a56b629203edc62c") {
        var items = data.concepts;
        if (items.length < maxItems) {
            maxItems = items.length;
            if (maxItems === 1) {
                formattedString = "The thing we are most confident in detecting is:";
            }
        } else {
            formattedString = "The " + maxItems + " things we are most confident in detecting are:";
        }

        for (var i = 0; i < maxItems; i++) {
            formattedString += "<br/>- " + items[i].name + " at a " + (Math.round(items[i].value * 10000) / 100) + "% probability";
        }
    }


    return formattedString;
}