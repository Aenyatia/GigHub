var GigsController = function (attendanceService) {
	var button;

	var fail = function () {
		alert("Something failed!");
	};

	var done = function () {
		const text = (button.text() === "Going") ? "Going?" : "Going";

		button.toggleClass("btn-info").toggleClass("btn-light").text(text);
	};

	var toggleAttendance = function (e) {
		button = $(e.target);
		const gigId = button.attr("data-gig-id");

		if (button.hasClass("btn-light"))
			attendanceService.createAttendance(gigId, done, fail);
		else
			attendanceService.deleteAttendance(gigId, done, fail);
	};

	const init = function (container) {
		$(container).on("click", ".js-toggle-attendance", toggleAttendance);
	};

	return {
		init: init
	};

}(AttendanceService);
