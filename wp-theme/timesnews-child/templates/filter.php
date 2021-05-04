<?php
/**
* Template Name: Filter Page
*
*/

$categories = get_categories( array(
    'orderby' => 'name',
    'order'   => 'ASC'
) );

get_header();
?>

<div class="wrap">
	<div id="primary" class="content-area">
		<main id="main" class="site-main">
			<header>
				<h1 class="page-title"><?php the_title(); ?></h1>
			</header>
			<div class="entry-content">
				<form action="<?php the_permalink(); ?>">
					<label>Select category:</label>
					<select name="cat">
						<option selected>-none-</option>
						<?php
							foreach ($categories as $category) {
								echo "<option value=" . $category->term_id . " >" . $category->name . "</option>";
							}
						?>
					</select>
					<label>Select discount:</label>
					<select name="dis">
						<option selected>-none-</option>
						
					</select>
					<label>Lower price:</label>
					<input type="text" name="lprice">
					<label>Upper price:</label>
					<input type="text" name="uprice">
					<input type="submit" value="Filter">
				</form>
			</div>
		</main>
	</div>
</div>

<?php
get_footer();