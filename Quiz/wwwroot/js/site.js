// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    // Initial setup for dropdowns, if needed
    initializeDropdowns();

    // Delegate events to dynamically loaded elements
    $(document).on('change', '#FacultyId, #EducationTypeId', function () {
        var facultyId = $('#FacultyId').val();
        var educationTypeId = $('#EducationTypeId').val();

        if (facultyId && educationTypeId) {
            $.ajax({
                url: '/Account/GetSpecializations',
                type: 'POST',
                data: {
                    facultyId: facultyId,
                    educationTypeId: educationTypeId
                },
                success: function (data) {
                    var $specializationDropdown = $('#SpecializationId');
                    $specializationDropdown.empty();
                    $specializationDropdown.append('<option value="">Select Specialization</option>');

                    if (data.length > 0) {
                        $.each(data, function (index, item) {
                            $specializationDropdown.append('<option value="' + item.Id + '">' + item.Name + '</option>');
                        });
                    } else {
                        $specializationDropdown.append('<option value="">No specializations available</option>');
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching specializations:', error);
                }
            });
        }
    });
});

