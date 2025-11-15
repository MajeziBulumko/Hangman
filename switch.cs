else
{
	
	death_count++;

	switch (death_count)
	{
		case 1:
			ground();
			pole();
			top();
			rope();
			head();
			break;
		
		case 2:
			ground();
			pole();
			top();
			rope();
			head();
			upper();
			break;
		
		
	}
	
	
	if(death_count == 5)
	{
		//dead code
	}
}