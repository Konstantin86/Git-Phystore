app.service('statusService', function() {

    // statuses valid values: success, warning, danger

    var state = {
        message: "",
        status: "success"
    };

    var set = function (msg, stat) {
        state.message = msg;
        state.status = stat;
    };

    var success = function (msg) {
        set(msg, "success");
    };

    var warning = function (msg) {
        set(msg, "warning");
    };

    var error = function (msg) {
        set(msg, "danger");
    };

    var clear = function () {
        state.message = "";
    };

    this.state = state;
    this.success = success;
    this.warning = warning;
    this.error = error;
    this.clear = clear;
});