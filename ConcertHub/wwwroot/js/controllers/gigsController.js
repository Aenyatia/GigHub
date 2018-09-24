﻿var GigsController = function (attendanceService) {
	var button;

	var fail = function () {
		alert("Something failed!");
	};

	var done = function () {
		var text = (button.text() === "Going") ? "Going?" : "Going";

		button.toggleClass("btn-info").toggleClass("btn-light").text(text);
	};

	var toggleAttendance = function (e) {
		button = $(e.target);
		var gigId = button.attr("data-gig-id");

		if (button.hasClass("btn-light"))
			attendanceService.createAttendance(gigId, done, fail);
		else
			attendanceService.deleteAttendance(gigId, done, fail);
	};

	var init = function (container) {
		$(container).on("click", ".js-toggle-attendance", toggleAttendance);
	};

	return {
		init: init
	};

}(AttendanceService);