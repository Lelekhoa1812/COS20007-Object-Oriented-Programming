import time

# Get user input
def get_initial_time():
    while True:
        choice = input('Start from "00:00:00"? Please type "yes" or "no: ')

        if choice.lower() == 'yes':
            return (0, 0, 0)
        elif choice.lower() == 'no':
            error = 'Invalid time format. Please try again.'
            custom_time = input('Type your preferred time to start in "hh:mm:ss" format: ')
            try:
                hh, mm, ss = map(int, custom_time.split(':'))
                if 0 <= hh < 24 and 0 <= mm < 60 and 0 <= ss < 60:
                    return (hh, mm, ss)
                else:
                    print(error)
            except:
                print(error)
        else:
            print('Invalid choice. Please type "yes" or "no".')

# Display digital clock function
def display_clock():
    hh, mm, ss = get_initial_time()
    
    while True:
        # Format timer as "hh : mm : ss"
        time_display = f"{hh:02d} : {mm:02d} : {ss:02d}"
        
        # Time display
        print(f"Time Display: {time_display}", end="\r")
        
        # Wait 1 second
        time.sleep(1)
        
        # Time update
        ss += 1
        if ss == 60:
            ss = 0
            mm += 1
            if mm == 60:
                mm = 0
                hh += 1
                if hh == 24:
                    hh = 0

display_clock()
