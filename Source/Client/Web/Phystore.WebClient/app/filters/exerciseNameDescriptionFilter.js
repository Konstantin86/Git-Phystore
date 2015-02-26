app.filter("exerciseNameDescriptionFilter", function () {
    return function (exercises, value) {
        if (!value) return exercises;

        var matches = [];
        value = value.toLowerCase();
        for (var i = 0; i < exercises.length; i++) {
            var exercise = exercises[i];

            if (exercise.name.toLowerCase().indexOf(value) > -1 ||
            exercise.description.toLowerCase().indexOf(value) > -1) {
                matches.push(exercise);
            }
        }
        return matches;
    };
});